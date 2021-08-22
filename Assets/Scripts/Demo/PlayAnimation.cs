using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct AnimationKeyPair
{
    public KeyCode keyCode;
    public string animationName;

}
public class PlayAnimation : MonoBehaviour
{
    public AnimationKeyPair[] animationKeyPairs;
    //
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        foreach (AnimationKeyPair aKP in animationKeyPairs)
        {
            if (Input.GetKeyDown(aKP.keyCode))
            {
                animator.CrossFade(aKP.animationName, 0.2f);

            }
            
        }
    }
}
