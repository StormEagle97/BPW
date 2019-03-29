using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUp : MonoBehaviour
{
    public int PickUpType = 1;
    private GameObject Player;
    private GameObject GameController;
    private GameRulingController GameRulingController;
    public static int IgnoreRaycastLayer;

    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player");

        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameRulingController = GameController.GetComponent<GameRulingController>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Om deze functie te kunnen triggeren moet je 2 box coliders maken. 1 zodat het object niet door de bodem zakt. Deze colider mak zeer klein zodat de speler er gemakkelijk in kan lopen
    // De 2de colider moet moet "is trigger" actief zijn. Deze moet net zo groot zijn als het object(kan ook een mesh colider zijn).
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            if (PickUpType == 1)
                Action1();
            else if (PickUpType == 2)
                Action2();
            else if (PickUpType == 3)
                Action3();
            else if (PickUpType == 4)
                Action4();
        }
    }

    void Action1()
    {
        GameRulingController.InstaKill();
    }

    void Action2()
    {
        GameObject Gun = GameObject.FindGameObjectWithTag("Gun");
        GunScript GunScript = Gun.GetComponent<GunScript>();
        GunScript.SetAmmoToFull();
    }

    void Action3()
    {
        FirstPersonController firstpersoncontroller = Player.GetComponent<FirstPersonController>();
        firstpersoncontroller.IncreaseSpeed();
        Debug.Log("Sending increase");

    }

    void Action4()
    {

    }
}
