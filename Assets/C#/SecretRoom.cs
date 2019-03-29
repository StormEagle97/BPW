using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class SecretRoom : MonoBehaviour
{
    public GameObject RemoveableObject;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void activate()
    {
        Destroy(RemoveableObject);
    }
}
