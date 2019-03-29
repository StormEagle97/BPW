using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public string SelectedWeapon;
    public string InventoryWeapon1;
    public string InventoryWeapon2;

    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        string PreviousSelectedWeapon = SelectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
            if (SelectedWeapon == InventoryWeapon1)
                SelectedWeapon = InventoryWeapon2;
            else
                SelectedWeapon = InventoryWeapon1;

        if (PreviousSelectedWeapon != SelectedWeapon)
            SelectWeapon();
    }

    public void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {
            if (weapon.name == SelectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
        }    
    }
}