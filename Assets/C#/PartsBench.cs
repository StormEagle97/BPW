using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsBench : MonoBehaviour
{
    bool border_collide = false;
    public bool ContainsPart1 = false;
    public bool ContainsPart2 = false;
    public bool ContainsPart3 = false;
    public bool ContainsPart4 = false;
    public bool ContainsPart5 = false;
    private GameObject GameController;
    private GameRulingController GameRulingController;
    public GameObject BenchPart1;
    public GameObject BenchPart2;
    public GameObject BenchPart3;
    public GameObject BenchPart4;
    public GameObject BenchPart5;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameRulingController = GameController.GetComponent<GameRulingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (border_collide && Input.GetKeyDown("f"))
        {
            if (GameRulingController.HasPart1 && !ContainsPart1)
            {
                //BenchPart1 = GameObject.FindGameObjectWithTag("BenchPart1");
                BenchPart1.SetActive(true);
                ContainsPart1 = true;
            }
            else if (GameRulingController.HasPart2 && !ContainsPart2)
            {
                //BenchPart2 = GameObject.FindGameObjectWithTag("BenchPart2");
                BenchPart2.SetActive(true);
                ContainsPart2 = true;
            }
            else if (GameRulingController.HasPart3 && !ContainsPart3)
            {
                //BenchPart3 = GameObject.FindGameObjectWithTag("BenchPart3");
                BenchPart3.SetActive(true);
                ContainsPart3 = true;
            }
            else if (GameRulingController.HasPart4 && !ContainsPart4)
            {
                //BenchPart4 = GameObject.FindGameObjectWithTag("BenchPart4");
                BenchPart4.SetActive(true);
                ContainsPart4 = true;
            }
            if (ContainsPart1 && ContainsPart2 && ContainsPart3 && ContainsPart4 && !ContainsPart5)
            {
                BenchPart1.SetActive(false);
                BenchPart2.SetActive(false);
                BenchPart3.SetActive(false);
                BenchPart4.SetActive(false);

                BenchPart5.SetActive(true);
                ContainsPart5 = true;
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
}
