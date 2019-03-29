using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuy : MonoBehaviour
{
    public int NormalPrice;
    public int RefillPrice;
    public string GunName;
    private GameObject player;
    private GameObject MainCamera;
    private GameObject Gun;
    private GunScript GunScript;
    private bool border_collide = false;
    private PointScript Points;
    private WeaponSwitching WeaponSwitching;
    private string WeaponSelected;
    private string Weapon1;
    private string Weapon2;
    private GameObject GameController;
    private GameRulingController GameRulingController;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            border_collide = true;
        GameRulingController.ControlShowBuyText("K98K", RefillPrice.ToString());

    }

    void OnTriggerExit(Collider col)
    {
        GameRulingController.ControlHideBuyText();
        if (col.tag == "Player")
            border_collide = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Points = player.GetComponent<PointScript>();

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        WeaponSwitching = MainCamera.GetComponentInChildren<WeaponSwitching>();

        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameRulingController = GameController.GetComponent<GameRulingController>();

        Gun = GameObject.Find(GunName);
        GunScript = Gun.GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (border_collide && Input.GetKey("f")){
            // Ophalen van current weapon switching settings
            Weapon1 = WeaponSwitching.InventoryWeapon1;
            Weapon2 = WeaponSwitching.InventoryWeapon2;
            WeaponSelected = WeaponSwitching.SelectedWeapon;

            if (GunName != Weapon1 && GunName != Weapon2){//Gun is not in inventory so we need to buy it and replace it with the weapon that is selected
                if (Points.Points >= NormalPrice){
                    WeaponSwitching.SelectedWeapon = GunName;
                    if (WeaponSwitching.InventoryWeapon1 == WeaponSelected)
                        WeaponSwitching.InventoryWeapon1 = GunName;
                    else if (WeaponSwitching.InventoryWeapon2 == WeaponSelected)
                        WeaponSwitching.InventoryWeapon2 = GunName;

                    WeaponSwitching.SelectWeapon();
                    Points.Points = Points.Points - NormalPrice;
                }
            }else{// The gun is in our inventory so refill it if it has less ammo than full.
                if (GunScript.Ammo != GunScript.MagSize || GunScript.AmmoLeft != GunScript.AmmoMax){
                    if (Points.Points >= RefillPrice){
                        GunScript.SetAmmoToFull();
                        Points.Points = Points.Points - RefillPrice;
                    }
                }
            }
        }
    }
}
