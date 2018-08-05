using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start ()
    {
        push = GameObject.FindGameObjectsWithTag(tagOfThePushable);
        pick = GameObject.FindGameObjectsWithTag(tagOfPickable);
        weak = GameObject.FindGameObjectsWithTag(tagOfTheWeak);



    }

    // Update is called once per frame
    void Update ()
    {

        
        timer+=Time.deltaTime;
        coolDownEffectTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q) && !effectActive && timer >= cooldownability)
        {
            foreach (GameObject obj in push)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 2);
                obj.GetComponent<Renderer>().material.SetColor("_OutlineColor", pushColor);
            }
            foreach (GameObject obj in pick)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 2);
                obj.GetComponent<Renderer>().material.SetColor("_OutlineColor", pickColor);

            }
            foreach (GameObject obj in weak)
            {
                obj.GetComponent<Renderer>().material.SetFloat("_OutlineSize", 2);
                obj.GetComponent<Renderer>().material.SetColor("_OutlineColor",weakColor);

            }
            // cube.GetComponent<Renderer>().material = effectMaterial;
            effectActive = true;
            coolDownEffectTimer = 0;
        }
        

        if(coolDownEffectTimer >= cooldowneffect&& effectActive)
        {
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
