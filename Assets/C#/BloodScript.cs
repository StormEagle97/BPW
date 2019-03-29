using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{
    private float index = 0;

    // Update is called once per frame
    void Update()
    {
        index += Time.deltaTime;
        if (index > 2)
            Destroy(gameObject);
    }
}
