using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLauncherBolt : MonoBehaviour
{
    public ParticleSystem MuzzleflashLauncher;
    private GunScript GunScript;
    private float index;
    private float ShotDelay;

    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        GunScript = parent.GetComponent<GunScript>();
        ShotDelay = GunScript.ShotDelay;
        index = Seconds(GunScript.ShotDelay);
    }

    // Update is called once per frame
    void Update()
    {
        index++;
        if (Input.GetButtonDown("Fire1") && GunScript.Ammo > 0 && index >= Seconds(ShotDelay))
        {
            index = 0;
            StartCoroutine(Flasher());
        }
    }

    IEnumerator Flasher()
    {
        MuzzleflashLauncher.Emit(10);
        yield return new WaitForSeconds(ShotDelay);
    }

    float Seconds(float seconds)
    {
        return seconds * 60.0f;
    }
}
