using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour 
{

	public GameObject dayoneGood;
    public GameObject dayoneBad;
    public GameObject daytwoGood;
    public GameObject daytwoBad;
    public GameObject daythreeGood;
    public GameObject daythreeBad;

	private string day;

	void Awake()
	{
		dayoneGood.SetActive(false);
		dayoneBad.SetActive(false);
		daytwoGood.SetActive(false);
		daytwoBad.SetActive(false);
		daythreeGood.SetActive(false);
		daythreeBad.SetActive(false);
	}

	// Use this for initialization
	void Start () 
	{
		day = PlayerPrefs.GetString("Day");
		if (day == "One")
		{
			string str = PlayerPrefs.GetString("One");
			if (str == "True")
			{
				dayoneGood.SetActive(true);
			}
			else if (str == "False")
			{
				dayoneBad.SetActive(true);
			}
		}
		else if (day == "Two")
		{
			string str = PlayerPrefs.GetString("Two");
			if (str == "True")
			{
				daytwoGood.SetActive(true);
			}
			else if (str == "False")
			{
				daytwoBad.SetActive(true);
			}
		}
		else if (day == "Three")
		{ 
			string str = PlayerPrefs.GetString("Three");
			if (str == "True")
			{
				daythreeGood.SetActive(true);
			}
			else if (str == "False")
			{
				daythreeBad.SetActive(true);
			}
		}
		//check for day then enable the door for it based on the option 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
