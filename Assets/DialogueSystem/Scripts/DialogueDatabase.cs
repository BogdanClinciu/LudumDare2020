using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "LudumDare_2020/DialogueDatabase", order = 0)]
public class DialogueDatabase : ScriptableObject
{
    public DialogueLine this[int key]
    {
        get
        {
            if(!intitialized)
            {
                Initialize();
            }

            if(Lines.ContainsKey(key))
            {
                return Lines[key];
            }
            else
            {
                Debug.Log($"{nameof(DialogueDatabase)} NO LINE FOR KEY: {key}");
                return defaultResponse;
            }
        }
    }

    [SerializeField]
    private DialogueLine defaultResponse;
    [SerializeField]
    private List<DialogueLine> lines;

    private Dictionary<int,DialogueLine> Lines;
    bool intitialized = false;

    private void Initialize()
    {
        defaultResponse = new DialogueLine();
        defaultResponse.ID = -1;
        defaultResponse.SpeakerSprite = null;
        defaultResponse.Text = "I don't know what to say dude.";

        Lines = new Dictionary<int, DialogueLine>();
        for (int i = 0; i < lines.Count; i++)
        {
            Lines.Add(lines[i].ID, lines[i]);
        }
    }

}

[System.Serializable]
public struct DialogueLine
{
    public int ID;
    public string Text;
    public Sprite SpeakerSprite;
}
