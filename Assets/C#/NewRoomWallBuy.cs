using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NewRoomWallBuy : MonoBehaviour

{
    public int Price;
    public string Door;
    private GameObject player;
    bool border_collide = false;
    private bool IHaveBeenHere = false;
    private Animator animator;
    private GameObject GameController;
    private GameRulingController GameRulingController;
    public GameObject SoundController;

    public Music_Effects MusicEffectsController;

    PointScript Points;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            border_collide = true;
            GameRulingController.ControlShowBuyText("Door", Price.ToString());
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            border_collide = false;
            GameRulingController.ControlHideBuyText();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Points = player.GetComponent<PointScript>();
        animator = GetComponent<Animator>();
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameRulingController = GameController.GetComponent<GameRulingController>();
        SoundController = GameObject.FindGameObjectWithTag("SoundPlayer");
        MusicEffectsController = SoundController.GetComponent<Music_Effects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (border_collide && Input.GetKey("f") && Points.Points >= Price)
        {
            StartCoroutine(DeleteDoor());
            if (!IHaveBeenHere)
            {
                GameRulingController.ReBakeMap();
                Points.Points = Points.Points - Price;
                MusicEffectsController.DoorSoundPlay();
                IHaveBeenHere = true;
                foreach (Transform child in gameObject.transform)
                    if (child.name == "Cube")
                        Destroy(child.gameObject);

                animator.SetBool("Open", true);
                
            }
        }
    }

    IEnumerator DeleteDoor()
    {
        GameRulingController.ControlHideBuyText();
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
    }
    
}
