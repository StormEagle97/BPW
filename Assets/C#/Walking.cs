using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    private Animation anim;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) 
            animator.SetTrigger("Moving");
        else if (Input.GetKey("s")) 
            animator.SetTrigger("Moving");
        else if (Input.GetKey("a")) 
            animator.SetTrigger("Moving");
        else if (Input.GetKey("d")) 
            animator.SetTrigger("Moving");
        else if (Input.GetMouseButton(1))
            animator.SetTrigger("Aiming");
        else if (Input.GetKey("r"))
            animator.SetTrigger("Reloading");
        else
            anim.Play("Stance");
    }
}