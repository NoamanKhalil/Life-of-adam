using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AiAnimator : MonoBehaviour 
{
    [Header("AI Teacher : EnemyNpcTeacher ,AI Bully : EnemyNpcBully ,AI Father : EnemyNpcFather")]
    [SerializeField]
    private string[] AiAnimStateName;
    [Header("AI Teacher : 1 ,AI Bully : 2 ,AI Father : 3")]
    [SerializeField]
    private int AiName;
    [Header(" either assign in unity or it will be assigned at runtime ")]
    [SerializeField]
    private Animator myAnim;

	// Use this for initialization
	void Start ()
    {
        if (!myAnim)
        {
            myAnim = GetComponent<Animator>();
        }

	}
    public void SetPatrol()
    {
        myAnim.SetInteger(AiAnimStateName[AiName], 1);
    }
    public void SetChase()
    {
        myAnim.SetInteger(AiAnimStateName[AiName], 2);
    }
}
