using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmoteDatabase", menuName = "LudumDare_2020/EmoteDatabase", order = 0)]
public class EmoteDatabase : ScriptableObject
{

    public Sprite this[EmoteID key]
    {
        get
        {
            if(!intitialized)
            {
                Initialize();
            }

            if(Emotes.ContainsKey(key))
            {
                return Emotes[key];
            }
            else
            {
                Debug.Log($"{nameof(EmoteDatabase)} NO EMOTE FOR KEY: {key}");
                return dialogueBubble;
            }
        }
    }

    public Sprite DialogueBubble => dialogueBubble;

    [SerializeField]
    private Sprite dialogueBubble;
    [SerializeField]
    private List<EmoteField> emotes;

    private Dictionary<EmoteID, Sprite> Emotes;
    private bool intitialized = false;

    private void Initialize()
    {
        Emotes = new Dictionary<EmoteID, Sprite>();
        for (int i = 0; i < emotes.Count; i++)
        {
            if(Emotes.ContainsKey(emotes[i].ID))
            {
                Debug.Log($"{nameof(EmoteDatabase)} Diplicate key for: {emotes[i].ID}");
            }
            else
            {
                Emotes.Add(emotes[i].ID, emotes[i].Sprite);
            }
        }
    }

#if UNITY_EDITOR
    public void SetSprites(List<Sprite> sprites)
    {
        Debug.Log(sprites.Count);
        for (int i = 0; i < sprites.Count; i++)
        {
            emotes.Add(new EmoteField(){ID = (EmoteID)i, Sprite = sprites[i]});
        }
    }
#endif
}


public enum EmoteID
{
	emote_alert = 0,
	emote_anger = 1,
	emote_bars = 2,
	emote_cash = 3,
	emote_circle = 4,
	emote_cloud = 5,
	emote_cross = 6,
	emote_dots1 = 7,
	emote_dots2 = 8,
	emote_dots3 = 9,
	emote_drop = 10,
	emote_drops = 11,
	emote_exclamation = 12,
	emote_exclamations = 13,
	emote_faceAngry = 14,
	emote_faceHappy = 15,
	emote_faceSad = 16,
	emote_heart = 17,
	emote_heartBroken = 18,
	emote_hearts = 19,
	emote_idea = 20,
	emote_laugh = 21,
	emote_music = 22,
	emote_question = 23,
	emote_sleep = 24,
	emote_sleeps = 25,
	emote_star = 26,
	emote_stars = 27,
	emote_swirl = 28,
	emote__ = 29,
}

[System.Serializable]
public struct EmoteField
{
    public EmoteID ID;
    public Sprite Sprite;
}
