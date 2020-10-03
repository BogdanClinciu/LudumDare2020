using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmoteObject : MonoBehaviour
{
    public bool IsShowing => isShowing;

    [SerializeField]
    private RectTransform baseTransform;
    [SerializeField]
    private Image emoteImage;

    private bool isShowing = false;
    private Vector3 targetWorldPos;


    public void ShowEmote(Sprite sprite, Transform targetTransform,  float duration, Camera targetCamera = null)
    {
        isShowing = true;

        if(targetCamera == null) targetCamera = Camera.main;

        emoteImage.sprite = sprite;
        StartCoroutine(ShowEmote(targetTransform, duration, targetCamera));
    }

    public void ShowEmote(Sprite sprite, Vector3 targetPos,  float duration, Camera targetCamera = null)
    {
        isShowing = true;
        targetWorldPos = targetPos;

        if(targetCamera == null) targetCamera = Camera.main;

        emoteImage.sprite = sprite;
        StartCoroutine(ShowEmote(targetPos, duration, targetCamera));
    }

    private IEnumerator ShowEmote(Transform targetTransform, float duration, Camera targetCamera)
    {
        baseTransform.position = targetCamera.WorldToScreenPoint(targetTransform.position + Vector3.up * EmoteManager.Instance.VerticalOffset);
        emoteImage.gameObject.SetActive(true);
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            if(baseTransform != null)
            {
                baseTransform.position = targetCamera.WorldToScreenPoint(targetTransform.position);
            }
            yield return null;
        }
        emoteImage.gameObject.SetActive(false);
        isShowing = false;
    }

    private IEnumerator ShowEmote(Vector3 targetPos, float duration, Camera targetCamera)
    {
        baseTransform.position = targetCamera.WorldToScreenPoint(targetPos + Vector3.up * EmoteManager.Instance.VerticalOffset);
        emoteImage.gameObject.SetActive(true);
        while (duration > 0)
        {
            baseTransform.position = targetCamera.WorldToScreenPoint(targetWorldPos);
            duration -= Time.deltaTime;
            yield return null;
        }
        emoteImage.gameObject.SetActive(false);
        isShowing = false;
    }
}
