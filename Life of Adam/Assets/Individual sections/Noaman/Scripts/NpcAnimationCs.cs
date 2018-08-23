using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class NpcAnimationCs : MonoBehaviour {

    [Header("Npc : NpcControl")]
    [SerializeField]
    private string AiAnimStateName;
    [Header("Default : 0 ,Interacted : 1")]
    [SerializeField]
    private int AnimStateNum;
    [Header(" either assign in unity or it will be assigned at runtime ")]
    [SerializeField]
    private Animator myAnim;

    // Use this for initialization
    void Start()
    {
        if (!myAnim)
        {
            myAnim = GetComponent<Animator>();
        }

    }
    public void SetInteractive()
    {
        myAnim.SetInteger(AiAnimStateName, AnimStateNum);
    }
}
