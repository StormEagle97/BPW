using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPowerPint : MonoBehaviour
{
    public int price;
    private bool PowerPint = false;
    private GameObject player;
    private GameObject Power;
    private PlayerScript PlayerScript;
    private PointScript PointScript;
    private PowerScript PowerScript;
    bool border_collide = false;

    private Light[] LightS;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Power = GameObject.FindGameObjectWithTag("Power");
        PlayerScript = player.GetComponent<PlayerScript>();
        PointScript = player.GetComponent<PointScript>();
        PowerScript = Power.GetComponent<PowerScript>();
        LightS = GetComponentsInChildren<Light>();
        foreach (Light Light in LightS)
            Light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //als speler in collision is met perk en power staat aan en points is genoeg

        if (border_collide && Input.GetKey("f") && PointScript.Points >= price && !PowerPint && PowerScript.Power)
        {
            PowerPint = true;
            PlayerScript.MaxHP = 5;
            PointScript.Points = PointScript.Points - price;
        }

        if (PowerScript.Power == true)
        {
            foreach (Light Light in LightS)
                Light.enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            border_collide = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
            border_collide = false;
    }
}
