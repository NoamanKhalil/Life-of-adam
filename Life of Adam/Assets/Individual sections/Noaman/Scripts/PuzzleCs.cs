using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleCs : MonoBehaviour
{
	[SerializeField]
	private GameObject shapeSlot;
    public float sphereRadius;
    public LayerMask layer;
    [SerializeField]
    private float Dist;
    [SerializeField]
    private GameObject otherObj;
    [SerializeField]
    private Text myDistText;
    [SerializeField]
    private string thisName;
    [SerializeField]
    private bool objective;
    [SerializeField]
    private GameObject player;

	public bool isBlue;
	public bool isRed;
    // Use this for initialization
    void Start ()
    {
        objective = false;
		shapeSlot.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Vector3.Distance(this.transform.position, player.transform.position) <= Dist)
		{
			Debug.Log("set can drop is working ");
			Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
			player.GetComponent<PickUpCs>().setCanDrop(true);
		}
		else if (Vector3.Distance(this.transform.position, player.transform.position) >= Dist)
		{ 
			player.GetComponent<PickUpCs>().setCanDrop(false);
		}
    }

	public void setSlotActive()
	{
		shapeSlot.SetActive(true);
	}


	public void Reset()
	{
		shapeSlot.SetActive(false);
		otherObj.SetActive(true);
		otherObj.GetComponent<objectHandlerCs>().Reset();
	}
    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }*/
}
