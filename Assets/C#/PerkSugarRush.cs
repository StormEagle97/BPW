using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkSugarRush : MonoBehaviour
{
    public int price;
    private bool SugarRush = false;
    private GameObject player;
    private GameObject Power;
    private PlayerScript PlayerScript;
    private PointScript PointScript;
    private PowerScript PowerScript;
    private GameObject Gun;
    private GunScript GunScript;
    private bool border_collide = false;
    private Animator animator;

    private Light[] LightS;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = player.GetComponent<PlayerScript>();
        PointScript = player.GetComponent<PointScript>();
        Gun = GameObject.FindGameObjectWithTag("Gun");
        GunScript = Gun.GetComponent<GunScript>();
        animator = Gun.GetComponent<Animator>();
        Power = GameObject.FindGameObjectWithTag("Power");
        PowerScript = Power.GetComponent<PowerScript>();
        LightS = GetComponentsInChildren<Light>();
        foreach (Light Light in LightS)
            Light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //als speler in collision is met perk en power staat aan en points is genoeg

        if (border_collide && Input.GetKey("f") && PointScript.Points >= price && !SugarRush && PowerScript.Power)
        {
            SugarRush = true;
            animator.SetBool("SugarRush", true);
            GunScript.ReloadTme = GunScript.ReloadTme / 2f;
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
