using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkJackpotJammer : MonoBehaviour
{
    public int price;
    private bool JackpotJammer = false;
    private GameObject Player;
    private GameObject Power;
    private PlayerScript PlayerScript;
    private PointScript PointScript;
    private PowerScript PowerScript;
    bool border_collide = false;

    private Light[] LightS;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Power = GameObject.FindGameObjectWithTag("Power");
        PlayerScript = Player.GetComponent<PlayerScript>();
        PointScript = Player.GetComponent<PointScript>();
        PowerScript = Power.GetComponent<PowerScript>();
        LightS = GetComponentsInChildren<Light>();
        foreach (Light Light in LightS)
            Light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (border_collide && Input.GetKey("f") && PointScript.Points >= price && !JackpotJammer && PowerScript.Power)
        {
            JackpotJammer = true;
            PointScript Points = Player.GetComponent<PointScript>();
            Points.PerkPoints = Points.PerkPoints + 60;
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
