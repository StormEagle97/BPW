using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour


{
    public int HP = 3;
    public int MaxHP = 3;
    private float index = 3;
    public GameObject DeathScreen;

    void Start()
    {

    }

    void Update()
    {
        index++;
        checkHP();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Zombie")
        {
            Zombie ZombieScript = col.transform.GetComponentInChildren<Zombie>();
            ZombieScript.StartAtacking();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Zombie")
        {
            Zombie ZombieScript = col.transform.GetComponentInChildren<Zombie>();
            ZombieScript.StopAtacking();
        }
    }


    public void TakeDamage()
    {
        HP --;
    }
    
    void checkHP()
    {
        if (HP <= 0)
            die();
        if (HP < MaxHP && index >= Seconds(5))
        {
            HP++;
            index = 0;
        }
    }

    void die()
    {
        DeathScreen.SetActive(true);
    }

    float Seconds(float seconds)
    {
        return seconds * 60f;
    }


}
