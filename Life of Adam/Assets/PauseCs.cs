﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCs : MonoBehaviour {

	public void OnPause()
	{
		Time.timeScale = 0;
		Cursor.visible = true;
	}
	public void OnPlay()
	{
		Time.timeScale = 1;
		Cursor.visible = false;
	}
}
