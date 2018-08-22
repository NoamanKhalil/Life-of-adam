using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TeacherAi : MonoBehaviour 
{
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
        myAnim.SetInteger("EnemyNpc", 1);
    }
    public void SetChase()
    {
        myAnim.SetInteger("EnemyNpc", 2);
    }
}
