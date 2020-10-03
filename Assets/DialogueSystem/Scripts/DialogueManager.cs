using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance => instance;
    private static DialogueManager instance = null;

    [SerializeField]
    private CanvasGroup dialogueParent;
    [SerializeField]
    private Image dialogueImage;
    [SerializeField]
    private AspectRatioFitter dialogueImageFitter;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    private DialogueLine[] steps;
    private int currentStep;
    private UnityAction onEnd;
    private bool isShowing = false;
    private WaitForSeconds inputDelay;


    private void Awake()
    {
        instance = this;
    }

    public void ShowSequence(params DialogueLine[] lines)
    {
        ShowSequence(null, lines);
    }

    public void ShowSequence(UnityAction onDialogueEnded, params DialogueLine[] lines)
    {
        if(lines == null || lines.Length == 0 || isShowing)
        {
            return;
        }

        isShowing = true;
        steps = lines;
        currentStep = -1;
        onEnd = onDialogueEnded;

        ShowNextStep();
        dialogueParent.interactable = true;
        dialogueParent.blocksRaycasts = true;
        dialogueParent.alpha = 1;
        StartCoroutine(ListenForInput());
    }

    public void ShowNextStep()
    {
        currentStep ++;
        if(currentStep >= steps.Length)
        {
            EndDialogueSequence();
            return;
        }

        dialogueText.text = steps[currentStep].Text;
        SetSprite(steps[currentStep].SpeakerSprite);
    }

    private void SetSprite(Sprite speakerSprite)
    {
        if(speakerSprite != null)
        {
            dialogueImageFitter.aspectRatio = (float)speakerSprite.texture.width/speakerSprite.texture.height;
        }
        dialogueImage.sprite = speakerSprite;
    }

    private void EndDialogueSequence()
    {
        isShowing = false;
        StopAllCoroutines();

        onEnd?.Invoke();
        dialogueParent.interactable = false;
        dialogueParent.blocksRaycasts = false;
        dialogueParent.alpha = 0;
    }

    private IEnumerator ListenForInput()
    {
        //wait one second before listening for keypresses
        yield return new WaitForSeconds(1);

        while (isShowing)
        {
            if(Input.anyKeyDown)
            {
                ShowNextStep();
            }
            yield return null;
        }
    }
}
