using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    private GameObject GameController;
    private GameRulingController GameRulingController;
    bool border_collide = false;
    public int PartNumber;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameRulingController = GameController.GetComponent<GameRulingController>();
    }

    // Update is called once per frame
    void Update()
    {
        index++;
        if (border_collide && Input.GetKey("f") && PartNumber != 5)
        {
            GameRulingController.FoundPart(PartNumber);
            Destroy(gameObject);
        }

        if (border_collide && Input.GetKey("f") && PartNumber == 5 )
        {
            if (index >= Seconds(1))
            {
                GameRulingController.FoundPart(PartNumber);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            border_collide = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
            border_collide = false;
    }

    int Seconds(int seconds)
    {
        return seconds * 60;
    }
}
