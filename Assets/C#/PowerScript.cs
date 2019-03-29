using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour

{
    private GameObject player;
    bool border_collide = false;
    private bool IHaveBeenHere = false;
    private Animator animator;
    public bool Power = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            border_collide = true;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            border_collide = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        animator.SetBool("PowerOn", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (border_collide && Input.GetKey("f"))
        {
            animator.SetBool("PowerOn", true);
            Power = true;
        }
    }
}
