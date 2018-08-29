using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
	public FpcontrollerCs fp;
    public float Sensitivity = 2f;
    public float Smoothness = 2f;
	public Vector2 lookAngle;

    Vector2 MouseControl;
	Vector2 Smoothing;
	GameObject Character;

    public float maxClamp;
    public float minClamp;

    bool isholding;
    bool canDrop;
	bool canMove;


    // Use this for initialization
    void Start () 
	{
		Character = this.transform.parent.gameObject;
        isholding = false;
		canMove = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	void Update () 
	{

		if (canMove && Time.timeScale== 1)
		{
			CameraMove();
		}
		else
		{
			//this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, new Vector3(25, 0, 0), Time.deltaTime*Smoothness);
		}
    }
	void CameraMove()
	{
		Vector2 nd = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		nd =Vector2.Scale (nd, new Vector2(Sensitivity* Smoothness, Sensitivity* Smoothness));
		Smoothing.x = Mathf.Lerp (Smoothing.x, nd.x, 1f / Smoothness);
		Smoothing.y = Mathf.Lerp (Smoothing.y, nd.y, 1f / Smoothness);
		MouseControl += Smoothing;
		MouseControl.y = Mathf.Clamp (MouseControl.y, minClamp, maxClamp);

		transform.localRotation = Quaternion.AngleAxis (-MouseControl.y, Vector3.right);
		Character.transform.localRotation = Quaternion.AngleAxis (MouseControl.x, Character.transform.up);
	}



    public void setCanDrop (bool drop)
    {

        canDrop = drop;
    }
    

    public bool isholdingCheck ()
    {
        return isholding;
    }

	public void canMoveCheck(bool con)
	{
		canMove = con;
	}
}
