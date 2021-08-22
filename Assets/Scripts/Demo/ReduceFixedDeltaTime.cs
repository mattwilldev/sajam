using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceFixedDeltaTime : MonoBehaviour
{
    private float fixedDeltaTime = 0.01f;
    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Time.fixedDeltaTime == fixedDeltaTime)
            {
                Time.fixedDeltaTime = fixedDeltaTime * 4;
            }
            else
            {
                Time.fixedDeltaTime = fixedDeltaTime;
            }
        }
    }

    private void OnDestroy()
    {
        Time.fixedDeltaTime = fixedDeltaTime;
    }
}
