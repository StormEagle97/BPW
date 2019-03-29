using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;
    public float Ammo;
    public float MagSize;
    public float ShotDelay;
    public float AmmoLeft;
    private bool Reload;
    private float ammoShot;
    private float index;


    private Animations_Script animationScript;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        ammoShot = Ammo;
        index = Seconds(ShotDelay);
        animationScript = Animations_Script.instance;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        index++;
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reloading());

        if (Input.GetMouseButtonDown(0))
        {
            if (Ammo > 0 && index >= Seconds(ShotDelay))
            {
                index = 0;
                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
                Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
                Destroy(Temporary_Bullet_Handler, 3.0f);

                Ammo -= 1;
                animator.SetBool("Shoot", true);
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && (Ammo == 0))
                {
                    //StartCoroutine(Reloading());
                    //ClickSound
                }
            }
        }
    }

    IEnumerator Reloading()
    {

        animator.SetBool("Reloading", true);
  //      animator.SetBool("Standing", false);

        yield return new WaitForSeconds(3.5f);

        if (AmmoLeft < MagSize)
        {
            if (Ammo != 0)
            {
                float meh = Ammo + AmmoLeft;
                AmmoLeft = meh - MagSize;
                Ammo = meh;
                if (AmmoLeft < 0)
                {
                    AmmoLeft = 0;
                }
                if (Ammo > 5)
                {
                    Ammo = 5;
                }
            }
            else
            {
                Ammo = AmmoLeft;
                AmmoLeft = 0;
            }
        }
        else
        {
            AmmoLeft = AmmoLeft - (ammoShot - Ammo);
            Ammo = 5;
        }

        index = 0;
        animator.SetBool("Reloading", false);
//        animator.SetBool("Standing", true);
    }

    float Seconds(float seconds)
    {
        return seconds * 60.0f;
    }
}
   
//    public IEnumerator Reload()// || (Input.GetKey("r")) // Reloading
