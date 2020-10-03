using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "LudumDare_2020/DialogueDatabase", order = 0)]
public class DialogueDatabase : ScriptableObject
{
    public string this[int key]
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
                return "Sorry. I dunno what to say.";
            }
        }
    }

    [SerializeField]
    private List<DialogueLine> lines;

    private Dictionary<int,string> Lines;
    bool intitialized = false;

    private void Initialize()
    {
        Lines = new Dictionary<int, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            Lines.Add(lines[i].ID, lines[i].Text);
        }
    }

}

public struct DialogueLine
{
    public int ID;
    public string Text;
}
