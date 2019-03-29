using UnityEngine;
using System;
using System.Collections;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {

    public string ZombieType;
    public float Speed = 0;
    public float HP = 100f;
    public int PointsForHit = 10;
    public int PointsForKill = 50;

    private Animation anim;
    private GameObject player;
    private Transform target;
    private NavMeshAgent agent;
    private GameObject GameController;
    private GameRulingController GameRulingController;
    public bool shouldMove = true;
    private bool IHaveBeenHere = false;
    private float TimeBeforeHit = 1.5f;
    private bool PlayerInHitRange = false;
    private float index = 0;

    Animator animator;
    Rigidbody m_Rigidbody;
    PointScript Points;

    // Use this for initialization
    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameRulingController = GameController.GetComponent<GameRulingController>();

        Points = player.GetComponent<PointScript>();
        
        target = player.transform;
        ZombieType = String.IsNullOrEmpty(ZombieType) ? ZombieType : "Default";
        Speed = Speed != 0 ? Speed : 50f;
        HP = HP != 0 ? HP : 5;
        animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
        checkHP();
        if (PlayerInHitRange)
        {
            index++;
            if (index >= Seconds(TimeBeforeHit))
            {
                PlayerScript PlayerScript = player.GetComponentInChildren<PlayerScript>();
                PlayerScript.TakeDamage();
                index = 0;
            }
        }
        else
        {
            index = 0;
        }

    }

    public void StartAtacking()
    {
        PlayerInHitRange = true;
    }

    public void StopAtacking()
    {
        PlayerInHitRange = false;
    }

    public void TakeDamage(float amount)
    {
        if (GameRulingController.Instakill)
            HP = 0;
        else
            HP -= amount;

        Points.Points = Points.Points + PointsForHit;
    }

    void checkHP()
    {
        if(HP <= 0)
            die();
    }
    
    void die()
    {
        animator.SetTrigger("Die");
        if (!IHaveBeenHere)
        {
            int Chance = UnityEngine.Random.Range(1, 100); // creates a number between 1 and 100
            if (Chance >= 99)// chance for a drop is 1 out of 100
                GameRulingController.SpawnPickUp(gameObject.transform);
            Points.Points = Points.Points + PointsForKill + Points.PerkPoints;
            IHaveBeenHere = true;
        }
        shouldMove = false;
        Destroy(gameObject, 9.5f);
        Destroy(GetComponentInChildren<MeshCollider>());
        Destroy(GetComponentInChildren<CapsuleCollider>());

    }

    float Seconds(float seconds)
    {
        return seconds * 60f;
    }
}
