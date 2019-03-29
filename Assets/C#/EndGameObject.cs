using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGameObject : MonoBehaviour
{
    public int price;
    bool border_collide = false;
    private GameObject Player;
    private PointScript PointScript;
    private bool bought = false;
    private int index = 0;
    private bool startcounter = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PointScript = Player.GetComponent<PointScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startcounter)
        {
            index++;
        }

        if (border_collide && Input.GetKey("f") && PointScript.Points >= price)
        {
            bought = true;
        }

        if (bought)
        {
            if (index >= Seconds(1))
                SceneManager.LoadScene("MainMenu");
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            border_collide = true;
            startcounter = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            border_collide = false;
            startcounter = false;
            index = 0;
        }
    }

    int Seconds(int seconds)
    {
        return seconds * 60;
    }
}
