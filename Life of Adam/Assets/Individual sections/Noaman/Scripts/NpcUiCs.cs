using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcUiCs : MonoBehaviour 
{
	public GameObject NpcText;
    public NpcAnimationCs npc;

	// Use this for initialization
	void Start () 
	{
		NpcText.SetActive(false);
	}
	void OnCollisionStay(Collision collisionInfo)
	{

		if (collisionInfo.gameObject.tag.Equals("Player"))
		{
			NpcText.SetActive(true);
            npc.SetInteractive();
		}
	}
}
