using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public float damage = 100f;
    public int range = 100;
    public string GunName;
    public int GunType; // 1 = pistol; 2 = pumpaction; 3 = bolt-action; 4 = single fire mag vet; 5 = full auto;

    public Camera fpsCam;
    public float Ammo;
    public float MagSize;
    public float ShotDelay;
    public float BoltDelay;
    public float AmmoLeft;
    public float AmmoMax;
    public float ReloadTme;
    public float HeadShotMultiplier = 1.5f;
    public bool IsReloading = false;
    public bool CanShoot = true;
    public Text AmmoDisplay;
    public Text AmmoLeftDisplay;
    public AudioClip ShootSound;
    public AudioClip ClickSound;
    public AudioClip BoltSound;
    public AudioClip ReloadSound;
    public GameObject partical;

    public ParticleSystem MuzzleFlash;

    private bool isSprinting = false;
    private float ammoShot;
    private float index;
    private float index2;
    private AudioSource m_AudioSource;
    private Animations_Script animationScript;
    private Animator animator;

    private float Seconds(float seconds)
    {
        //return seconds * 60.0f;
        return seconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        ammoShot = Ammo;
        index = Seconds(ShotDelay);
        animationScript = Animations_Script.instance;
        animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        index += Time.deltaTime;
        index2 += Time.deltaTime;
        AmmoDisplay.text = Ammo.ToString();
        AmmoLeftDisplay.text = AmmoLeft.ToString();
       
        if (GunType == 1)
            checkMovementGuntype1();// check which guntype is present
        else if (GunType == 3)
            checkMovementGuntype3();
        else if (GunType == 5)
            checkMovementGuntype5();
    }

    void checkMovementGuntype1()
    {
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("s") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("a") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("d") && Input.GetKey(KeyCode.LeftShift))
        {//Sprinting
            animator.SetBool("Sprinting", true);
            animator.SetBool("Shoot", false);
            CanShoot = false;
            isSprinting = true;
            index2 = 0;
        }
        else if (Input.GetKey("w") && Input.GetMouseButtonDown(0) || Input.GetKey("a") && Input.GetMouseButtonDown(0) || Input.GetKey("s") && Input.GetMouseButtonDown(0) || Input.GetKey("d") && Input.GetMouseButtonDown(0)) // Walking and Shooting
        {//walking and shooting
            if (Ammo > 0 && IsReloading == false && CanShoot && index >= Seconds(ShotDelay))
            {//if player has ammo
                animator.SetBool("Moving", true);
                Shoot();
            }
            else if (Ammo == 0)
                empty();
            else
                animator.SetBool("Moving", true);
        }
        else if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {// Moving
            if (index2 >= Seconds(0.35f))
            {
                index2 = 0;
                isSprinting = false;
                CanShoot = true;
            }
            if (Input.GetKeyDown("r") && AmmoLeft > 0)
            {
                StartCoroutine(Reloading());
            }

            animator.SetBool("Moving", true);
            animator.SetBool("Sprinting", false);
            animator.SetBool("Shoot", false);
        }
        else if (Input.GetMouseButtonDown(0) && Ammo > 0 && CanShoot && index >= Seconds(ShotDelay))
        { //Shooting
            Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && Ammo == 0)
        { //Shooting when empty
            empty();
        }
        else if (Input.GetKeyDown("r") && AmmoLeft > 0) // Reloading
        {
            StartCoroutine(Reloading());
        }
        else
        { // Standing still
            animator.SetBool("Sprinting", false);
            animator.SetBool("Moving", false);
            CanShoot = true;
        }
    }

    void checkMovementGuntype3()
    {
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("s") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("a") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("d") && Input.GetKey(KeyCode.LeftShift))
        {//Sprinting
            animator.SetBool("Sprinting", true);
            animator.SetBool("Shoot", false);
            CanShoot = false;
            isSprinting = true;
            index2 = 0;
        }
        else if (Input.GetKey("w") && Input.GetMouseButtonDown(0) || Input.GetKey("a") && Input.GetMouseButtonDown(0) || Input.GetKey("s") && Input.GetMouseButtonDown(0) || Input.GetKey("d") && Input.GetMouseButtonDown(0)) // Walking and Shooting
        {//walking and shooting
            if (Ammo > 0 && IsReloading == false && CanShoot && index >= Seconds(ShotDelay))
            {//if player has ammo
                animator.SetBool("Moving", true);
                Shoot();
            }
            else if (Ammo == 0)
                empty();
            else
            {
                animator.SetBool("Moving", true);
                animator.SetBool("Bolt", false);
            }
        }
        else if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {// Moving
            if (!CanShoot && index2 >= Seconds(0.35f))
            {
                index2 = 0;
                isSprinting = false;
                CanShoot = true;
            }
            if (Input.GetKeyDown("r") && AmmoLeft > 0)
            {
                StartCoroutine(Reloading());
            }

            animator.SetBool("Moving", true);
            animator.SetBool("Sprinting", false);
            animator.SetBool("Shoot", false);
        }
        else if (Input.GetMouseButtonDown(0) && Ammo > 0 && CanShoot && index >= Seconds(ShotDelay))
        { //Shooting
            Shoot();
        }
        else if (Input.GetMouseButton(0) && Ammo == 0)
        { //Shooting when empty
            empty();
        }
        else if (Input.GetKeyDown("r") && AmmoLeft > 0) // Reloading
        {
            StartCoroutine(Reloading());
            animator.SetBool("Bolt", false);
            CanShoot = false;
        }
        else
        { // Standing still
            animator.SetBool("Sprinting", false);
            animator.SetBool("Moving", false);
            CanShoot = true;
        }
    }

    void checkMovementGuntype5()
    {
        index2++;
        if (Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("s") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("a") && Input.GetKey(KeyCode.LeftShift) || Input.GetKey("d") && Input.GetKey(KeyCode.LeftShift))
        {//Sprinting
            animator.SetBool("Sprinting", true);
            animator.SetBool("Shoot", false);
            CanShoot = false;
            isSprinting = true;
            index2 = 0;
        }
        else if (Input.GetKey("w") && Input.GetMouseButton(0) || Input.GetKey("a") && Input.GetMouseButton(0) || Input.GetKey("s") && Input.GetMouseButton(0) || Input.GetKey("d") && Input.GetMouseButton(0)) // Walking and Shooting
        {//walking and shooting
            if (Ammo > 0 && IsReloading == false && CanShoot && index >= Seconds(ShotDelay))
            {//if player has ammo
                animator.SetBool("Moving", true);
                Shoot();
            }
            else if (Ammo == 0)
                empty();
            else
            {
                animator.SetBool("Moving", true);
            }
        }
        else if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {// Moving
            Debug.Log(index2 >= Seconds(0.35f));
            if (index2 >= Seconds(0.35f))
            {
                index2 = 0;
                isSprinting = false;
                CanShoot = true;
            }
            if (Input.GetKeyDown("r") && AmmoLeft > 0)
            {
                StartCoroutine(Reloading());
            }

            animator.SetBool("Moving", true);
            animator.SetBool("Sprinting", false);
            animator.SetBool("Shoot", false);
        }
        else if (Input.GetMouseButton(0) && Ammo > 0 && index >= Seconds(ShotDelay))
        { //Shooting
            Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && Ammo == 0)
        { //Shooting when empty
            empty();
        }
        else if (Input.GetKeyDown("r") && AmmoLeft > 0) // Reloading
        {
            StartCoroutine(Reloading());
        }
        else
        { // Standing still
            animator.SetBool("Sprinting", false);
            animator.SetBool("Shoot", false);
            animator.SetBool("Moving", false);
            CanShoot = true;
        }
    }

    private void Shoot()
    {
        index = 0;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Zombie Zombie = hit.transform.GetComponent<Zombie>();
            if (Zombie != null)
            {
                GameObject new_partical = partical;
                new_partical.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                Instantiate(new_partical);
                if (hit.point.y >= 1.6)
                    Zombie.TakeDamage(damage * HeadShotMultiplier);
                else
                    Zombie.TakeDamage(damage);
            }

            GameObject gameobject = hit.transform.gameObject;
            if (gameobject.tag == "Trigger")
            {
                SecretRoom SecretRoom = gameobject.GetComponentInChildren<SecretRoom>();
                SecretRoom.activate();
            }
        }
        m_AudioSource.clip = ShootSound;
        m_AudioSource.PlayOneShot(ShootSound);

        StartCoroutine(Fire());
    }

    private void empty()
    {
        m_AudioSource.clip = ClickSound;
        m_AudioSource.PlayOneShot(ClickSound);
    }

    public void SetAmmoToFull()
    {
        AmmoLeft = AmmoMax;
        Ammo = MagSize;
    }

    // All IEnumerators are on the bottom

    IEnumerator Fire()
    {
        if (!animator.GetBool("Shoot"))
        {
            if (Ammo > 1)
            {
                animator.SetBool("Shoot", true);
                animator.SetBool("Bolt", true);
                Ammo -= 1;
                StartCoroutine(QueueBoltSound());
                yield return new WaitForSeconds(ShotDelay);
                animator.SetBool("Shoot", false);
                animator.SetBool("Bolt", false);
            }
            else if (Ammo == 1)
            {
                animator.SetBool("Shoot", true);
                Ammo -= 1;
                yield return new WaitForSeconds(ShotDelay - BoltDelay);
                animator.SetBool("Shoot", false);
            }
        }
    }


    // This function starts the bolt sound
    IEnumerator QueueBoltSound(){
        yield return new WaitForSeconds(.4f);
        m_AudioSource.clip = BoltSound;
        m_AudioSource.PlayOneShot(BoltSound);
    }

    // This function start the reload animation and sets the ammo count.
    IEnumerator Reloading(){
        IsReloading = true;
        CanShoot = false;

        m_AudioSource.clip = ReloadSound;
        m_AudioSource.PlayOneShot(ReloadSound);

        animator.SetBool("Shoot", false);
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(ReloadTme);
        animator.SetBool("Reloading", false);

        IsReloading = false;
        CanShoot = true;

        if (AmmoLeft < MagSize){
            if (Ammo != 0){
                float result = Ammo + AmmoLeft;
                AmmoLeft = result - MagSize;
                Ammo = result;
                if (AmmoLeft < 0)
                    AmmoLeft = 0;
                if (Ammo > MagSize)
                    Ammo = MagSize;
            }else{
                Ammo = AmmoLeft;
                AmmoLeft = 0;
            }
        }else{
            AmmoLeft = AmmoLeft - (ammoShot - Ammo);
            Ammo = MagSize;
        }

        index = Seconds(ShotDelay - 0.2f);
    }
}
