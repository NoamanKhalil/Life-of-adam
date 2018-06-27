using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : MonoBehaviour {


	/// <summary>
	/// The is day one.
	/// </summary>

    public bool isDayOneBool;
    public bool isDayTwoBool;
    public bool isDayThreeBool;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// good choice pass true bad false 
	public void isDayOne(bool isTrue)
	{

		PlayerPrefs.SetString("Day", "One");
		if (isTrue == true)
		{
			PlayerPrefs.SetString("One", "True");
		}
		else if (isTrue == false)
		{
			PlayerPrefs.SetString("One", "False");
		}
	}
	public void isDayTwo(bool isTrue)
	{
		PlayerPrefs.SetString("Day", "Two");
		if (isTrue == true)
		{
			PlayerPrefs.SetString("Two", "True");
		}
		else if (isTrue == false)
		{
			PlayerPrefs.SetString("Two", "False");
		}
	}
	public void isDayThree(bool isTrue)
	{
		PlayerPrefs.SetString("Day", "Three");
		
		if (isTrue == true)
		{
			PlayerPrefs.SetString("Three", "True");
		}
		else if (isTrue == false)
		{
			PlayerPrefs.SetString("Three", "False");
		}
	}
		
}
