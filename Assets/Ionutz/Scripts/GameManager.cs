using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
	private void Awake()
	{
        if (Instance == null)
            Instance = this;
	}

    public float FixedDeltaTime = 0f;
    public float DeltaTime = 0f;
    public bool TimeFlow = true;

    // Update is called once per frame
    void Update()
    {
        if (TimeFlow)
            DeltaTime = Time.deltaTime;
    }

	private void FixedUpdate()
	{
        if (TimeFlow)
            FixedDeltaTime = Time.fixedDeltaTime;
    }
}
