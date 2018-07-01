using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
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
        if (Vector3.Distance(this.transform.position, player.transform.position) < Dist)
        {
            player.GetComponentInChildren<CameraControl>().setCanDrop(true);
        }
    }

	public void setSlotActive()
	{
		shapeSlot.SetActive(true);
	}

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }*/
}
