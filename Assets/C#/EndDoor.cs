using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    public bool ContainsEndPart = false;
    private GameObject GameController;
    private GameRulingController GameRulingController;
    private PointScript PointScript;
    private GameObject Player;
    public GameObject endPart;
    bool border_collide = false;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameRulingController = GameController.GetComponent<GameRulingController>();

        Player = GameObject.FindGameObjectWithTag("Player");
        PointScript = Player.GetComponent<PointScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (border_collide && Input.GetKey("f"))
        {
            if (GameRulingController.HasPart5 && !ContainsEndPart)
            {
                endPart.SetActive(true);
                ContainsEndPart = true;
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
