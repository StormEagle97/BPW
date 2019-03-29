using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameRulingController : MonoBehaviour
{
    public GameObject[] spawns;
    public GameObject[] Zombies;
    public GameObject[] PickUps;

    public GameObject spawn;
    public GameObject enemy;
    public GameObject SoundController;
    public GameObject popover;
    public GameObject popoverName;
    public GameObject popoverPrice;
    public NavMeshSurface surfaces;
    public NavMeshData NavMeshData;

    public Music_Effects MusicEffectsController;

    public Vector3 m_Size = new Vector3(80.0f, 20.0f, 80.0f);

    public bool Instakill = false;
    public bool HasPart1 = false;
    public bool HasPart2 = false;
    public bool HasPart3 = false;
    public bool HasPart4 = false;
    public bool HasPart5 = false;
    public bool ZombieSpawns = true;

    public int WaveNr = 1;

    private int index = 0;
    private int ZombiesAWave = 6;
    private int ZombiesSpawned = 0;

    // Use this for initialization
    void Start() {
        SoundController = GameObject.FindGameObjectWithTag("SoundPlayer");
        MusicEffectsController = SoundController.GetComponent<Music_Effects>();
        surfaces.UpdateNavMesh(NavMeshData);
    }

    // Update is called once per frame
    void Update ()
    {
        Zombies = GameObject.FindGameObjectsWithTag("Zombie");
        spawns = GameObject.FindGameObjectsWithTag("Respawn");

        index++;
        Wave();
    }

   void Wave()
    {
        if (ZombiesSpawned < ZombiesAWave)
        {
            if (index == Seconds(1) && Zombies.Length < 40 && ZombieSpawns && spawns.Length > 0 )
            {
                GameObject NewEnemy = enemy;
                int Chosen = Random.Range(1, spawns.Length); // creates a number between 1 and the amount of spawns
                spawn = GameObject.Find(spawns[Chosen -1].name);
                NewEnemy.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z);
                Instantiate(NewEnemy);
                ZombiesSpawned++;
                index = 0;
            }
        }
        else
        {
            if (Zombies.Length == 0)
            {
                MusicEffectsController.WaveSoundPlay();
                WaveNr++;
                ZombiesAWave = ZombiesAWave + (3 * WaveNr);
                ZombiesSpawned = 0;
                index = 0;
            }
        }
    }

    public void InstaKill()
    {
        StartCoroutine(InstaKillIE());
    }

    IEnumerator InstaKillIE()
    {
        Instakill = true;
        yield return new WaitForSeconds(5);
        Instakill = false;
    }

    public void SpawnPickUp(Transform ZombieTransform)
    {
        GameObject PickUp;
        int ChosenPickUp = Random.Range(0, PickUps.Length); // creates a number between 0 and the amount of PickUps
        PickUp = PickUps[ChosenPickUp];
        PickUp.transform.position = new Vector3(ZombieTransform.position.x, ZombieTransform.position.y + 2, ZombieTransform.position.z);
        Instantiate(PickUp);
    }
    
    public void FoundPart(int NumberFound)
    {
        if (NumberFound == 1)
            HasPart1 = true;
        else if (NumberFound == 2)
            HasPart2 = true;
        else if (NumberFound == 3)
            HasPart3 = true;
        else if (NumberFound == 4)
            HasPart4 = true;
        else if (NumberFound == 5)
            HasPart5 = true;
    }

    public void ControlShowBuyText(string name, string price)
    {
        foreach (Transform child in popover.transform)
        {
            if (child.name == "Name")
            {
                Text nameScript = child.GetComponent<Text>();
                nameScript.text = name;
            }
            if (child.name == "Price")
            {
                Text priceScript = child.GetComponent<Text>();
                priceScript.text = price;
            }
        }
        popover.SetActive(true);


    }

    public void ControlHideBuyText()
    {
        popover.SetActive(false);
    }

    public void ReBakeMap()
    {
        StartCoroutine(IEReBake());
    }

    IEnumerator IEReBake()
    {
        yield return new WaitForSeconds(1f);
        surfaces.UpdateNavMesh(NavMeshData);
    }

    int Seconds(int seconds){
        return seconds * 60;
    }
}