using System.Collections.Generic;
using UnityEngine;

public class EmoteManager : MonoBehaviour
{
    public static EmoteManager Instance => instance;
    private static EmoteManager instance = null;

    public float VerticalOffset => verticalOffset;

    [SerializeField]
    private float verticalOffset = 1f;
    [SerializeField]
    private float defaultDuration = 1f;

    [SerializeField][Space]
    private GameObject emoteObjectPrefab;
    [SerializeField]
    private Transform emoteObjectsParent;

    [SerializeField][Space]
    private EmoteDatabase emoteDatabase;

    private List<EmoteObject> emotePool = new List<EmoteObject>();


    private void Awake()
    {
        instance = this;
    }

    ///<summary>
    /// Show an emote that follows the specified Transform
    ///</summary>
    public void ShowEmote(Transform targetTransform, EmoteID emoteID, float duration = -1, Camera camera = null)
    {
        if(duration < 0) duration = defaultDuration;

        FreeObject.ShowEmote(emoteDatabase[emoteID], targetTransform, duration, camera);
    }

    ///<summary>
    /// Show an emote that stays at the specific provided world point
    ///</summary>
    public void ShowEmote(Vector3 targetPos, EmoteID emoteID, float duration = -1, Camera camera = null)
    {
        if(duration < 0) duration = defaultDuration;

        FreeObject.ShowEmote(emoteDatabase[emoteID], targetPos, duration, camera);
    }

    private EmoteObject FreeObject
    {
        get
        {
            for (int i = 0; i < emotePool.Count; i++)
            {
                if(!emotePool[i].IsShowing)
                    return emotePool[i];
            }

            EmoteObject newEmoteObj = Instantiate(emoteObjectPrefab, emoteObjectsParent).GetComponent<EmoteObject>();
            emotePool.Add(newEmoteObj);
            return newEmoteObj;
        }
    }
}
