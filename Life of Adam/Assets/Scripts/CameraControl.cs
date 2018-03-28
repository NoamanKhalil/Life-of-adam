using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
	Vector2 MouseControl;
	Vector2 Smoothing;
	public float Sensitivity = 2f;
	public float Smoothness = 2f;

	GameObject Character;

	// Use this for initialization
	void Start () 
	{
		Character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		var nd = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		nd =Vector2.Scale (nd, new Vector2 (Sensitivity * Smoothness, Sensitivity * Smoothness));
		Smoothing.x = Mathf.Lerp (Smoothing.x, nd.x, 1f / Smoothness);
		Smoothing.y = Mathf.Lerp (Smoothing.y, nd.y, 1f / Smoothness);
		MouseControl += Smoothing;
		MouseControl.y = Mathf.Clamp (MouseControl.y, -20f, 60f);
	//	MouseControl.x = Mathf.Clamp (MouseControl.y, -20f, 60f);

		transform.localRotation = Quaternion.AngleAxis (-MouseControl.y, Vector3.right);
		//transform.rotation = Quaternion.AngleAxis (-MouseControl.y, Vector3.right);
		Character.transform.localRotation = Quaternion.AngleAxis (MouseControl.x, Character.transform.up);
		//Character.transform.rotation = Quaternion.AngleAxis (MouseControl.x, Character.transform.up);
	}
}
