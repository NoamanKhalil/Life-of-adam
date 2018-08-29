using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcUiCs : MonoBehaviour 
{
    [SerializeField]
    private GameObject thingToEnable;
    public GameObject NpcText;
    public NpcAnimationCs npc;
    private LevelManagerCs lv;
	// Use this for initialization
	void Start () 
	{
        
        lv= Object.FindObjectOfType<LevelManagerCs>();
        NpcText.SetActive(false);
        thingToEnable.SetActive(false);
    }
	void OnTriggerEnter(Collider other)
	{

        GameObject.Find("PauseHandler").SetActive(false);
        Object.FindObjectOfType<PauseCs>().OnPause();
        thingToEnable.SetActive(true);
        NpcText.SetActive(true);
        // npc.SetInteractive();
        this.gameObject.SetActive(false);


    }

}
