using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandlerCs : MonoBehaviour
{
    [Header("Pass in canvas with canvas group so fade in can work")]
	public CanvasGroup can;
    [Header("controls how smooth the fade is ")]
	public float smooth;
	public GameObject myCanvas;
	public bool isPaused;
	private bool isIsRunningCoroutine;

	// Use this for initialization
	void Start ()
	{
		//myCanvas.SetActive(false);
		isPaused = false;
		can.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			if (isPaused)
			{
				isPaused = false;
			}
			else
			{
				isPaused = true;
			}
		}
		//if is pasued is flase
	    if (!isPaused)
		{
			
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
            StopAllCoroutines();
			StartCoroutine(uiFadein(can));
			Time.timeScale = 1;
			return;


		}
		//is is pasued is true
		else if (isPaused)
		{
			
			//myCanvas.SetActive(true);
			Cursor.visible = true;
	        Cursor.lockState = CursorLockMode.None;
            StopAllCoroutines();
			StartCoroutine(uiFadeout(can));
			Time.timeScale = 0;
			return;
		}
	}


	private IEnumerator uiFadein(CanvasGroup cg)
	{
		isIsRunningCoroutine = true;
		while (can.alpha >= 0)
		{
			can.alpha -= Time.fixedDeltaTime / smooth;

		    isIsRunningCoroutine = false;
			yield return null;
		}

		isIsRunningCoroutine = false;
		yield return null;


	}
	private IEnumerator uiFadeout(CanvasGroup cg)
	{
		isIsRunningCoroutine = true;
		while (can.alpha <= 1)
		{
			can.alpha += Time.fixedDeltaTime / smooth;
		    isIsRunningCoroutine = false;
			yield return null;
		}
		isIsRunningCoroutine = false;
		yield return null;	
	}

	public void unPause()
	{
		isPaused = false;
	}
}
