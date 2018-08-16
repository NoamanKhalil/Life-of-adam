using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerShader : MonoBehaviour
{
    [Header("your objects")]
    public GameObject[] pick;
    public GameObject[] push;
    public GameObject[] weak;
    public bool effectActive;
    [Header("this is for the use of the ability")]
    public float cooldownability;
    [Header("this is how long the effect stays")]
    public float cooldowneffect;
    [Header("Ignore These")]
    public float coolDownEffectTimer;
    public float timer;
    [Header("The tags of your objects")]
    public string tagOfPickable;
    public string tagOfThePushable;
    public string tagOfTheWeak;
    [Header("The Colors for the outline color you'd like")]
    public Color weakColor = Color.red;
    public Color pushColor = Color.green;
    public Color pickColor = Color.yellow;
    [Header("refrence to the image component")]
	public Image powerUI;
	private float powerUiAlpha;
    [Header("AudioSourceOnCamera")]
    public AudioSource aud;
    [Header("0 start audio for ability , 1 deactivate ")]
    public AudioClip[] clip;
	// Use this for initialization
	void Start()
	{
		push = GameObject.FindGameObjectsWithTag(tagOfThePushable);
		pick = GameObject.FindGameObjectsWithTag(tagOfPickable);
		weak = GameObject.FindGameObjectsWithTag(tagOfTheWeak);
		powerUiAlpha = 1;
		timer = cooldownability;
	}

    // Update is called once per frame
    void Update ()
    {
		timer += Time.deltaTime;
	    // why is it counting up ?
		coolDownEffectTimer += Time.deltaTime;
		powerUI.fillAmount = powerUiAlpha;
    


		// when active UI alpha will be 1 
		if (!effectActive)
		{
			powerUiAlpha = Mathf.Lerp(powerUiAlpha, 1, Time.deltaTime);
		}
		//wehn inactive the alpha value will decrese to 0 
		else if (effectActive)
		{
			powerUiAlpha = Mathf.Lerp(powerUiAlpha, 0, Time.deltaTime);
		}
		// adjust alpha to be 

        if (Input.GetKeyDown(KeyCode.Q) && !effectActive && timer >= cooldownability)
        {
            if (!aud.isPlaying)
            {
                aud.clip = clip[0];
                aud.Play();
            }
            else 
            {
                aud.Pause();
            }
            foreach (GameObject obj in push)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 1.1f);
                obj.GetComponent<Renderer>().material.SetColor("_OutlineColor", pushColor);
            }
            foreach (GameObject obj in pick)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 1.1f);
                obj.GetComponent<Renderer>().material.SetColor("_OutlineColor", pickColor);

            }
            foreach (GameObject obj in weak)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 1.1f);
                obj.GetComponent<Renderer>().material.SetColor("_OutlineColor",weakColor);

            }
            // cube.GetComponent<Renderer>().material = effectMaterial;
            effectActive = true;
            coolDownEffectTimer = 0;
        }
        if(coolDownEffectTimer >= cooldowneffect&& effectActive)
        {

            if (!aud.isPlaying)
            {
                aud.clip = clip[3];
                aud.Play();
            }
            else
            {
                aud.Pause();
            }
            foreach (GameObject obj in push)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 0);
                
            }
            foreach (GameObject obj in pick)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 0);
                
            }
            foreach (GameObject obj in weak)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 0);
            }
            effectActive = false;
           // cube.GetComponent<Renderer>().material = defaultMat;
            timer = 0;
            coolDownEffectTimer = 0;

        }
        

      
       
        

    }
 
}
