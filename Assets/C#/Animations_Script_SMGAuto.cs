using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Animations_Script_SMGAuto : MonoBehaviour
{
    #region Singleton

    public static Animations_Script_SMGAuto instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    private Animation anim;
    private GunScript GunScript;
    public Animator animator;
    public bool isSprinting = false;
    private float index;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        animator = GetComponent<Animator>();
    
        FirstPersonController firstpersoncontroller = GetComponent<FirstPersonController>();
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");
        GunScript = gun.GetComponent<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        index++;
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("s") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("a") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("d") && Input.GetKey(KeyCode.LeftShift))
        {//Sprinting
            animator.SetBool("Sprinting", true);
            animator.SetBool("Shoot", false);
            GunScript.CanShoot = false;
            isSprinting = true;
            index = 0;
        }
        else if (Input.GetKey("w") && Input.GetMouseButton(0) || Input.GetKey("a") && Input.GetMouseButton(0) || Input.GetKey("s") && Input.GetMouseButton(0) || Input.GetKey("d") && Input.GetMouseButton(0)) // Walking and Shooting
        {//walking and shooting
            if (GunScript.Ammo > 0 && GunScript.IsReloading == false && GunScript.CanShoot)
            {//if player has ammo
                animator.SetBool("Moving", true);
                StartCoroutine(AutoFire());
            }
            else
            {// if player is empty
                animator.SetBool("Moving", true);
            }
        }
        else if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {// Moving
            if (index >= Seconds(0.35f))
            {
                index = 0;
                isSprinting = false;
                GunScript.CanShoot = true;
            }

            animator.SetBool("Moving", true);
            animator.SetBool("Sprinting", false);
            animator.SetBool("Shoot", false);
        }
        else if (Input.GetMouseButton(0) && GunScript.Ammo > 0)
        { //Shooting
            StartCoroutine(AutoFire());
        }
        else if (Input.GetMouseButton(0) && GunScript.Ammo == 0)
        { //Shooting when empty
            animator.SetBool("Standing", true);
        }
        else if (Input.GetKeyDown("r") && GunScript.AmmoLeft > 0) // Reloading
        {
            animator.SetBool("Reloading", true);
            animator.SetBool("Shoot", false);
            GunScript.CanShoot = false;
        }
        else
        { // Standing still
            animator.SetBool("Sprinting", false);
            animator.SetBool("Shoot", false);
            animator.SetBool("Moving", false);
            GunScript.CanShoot = true;
        }
    }

    IEnumerator AutoFire()
    {
        Debug.Log("firing");
        if (!animator.GetBool("Shoot"))
        {
            animator.SetBool("Shoot", true);
            yield return new WaitForSeconds(GunScript.ShotDelay);
            animator.SetBool("Shoot", false);
        }
        
    }
    float Seconds(float seconds)
    {
        return seconds * 60.0f;
    }
}