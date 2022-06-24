using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool Died = false;
    public static bool interaction;
    public float InputX, InputZ;
    public Vector3 dirMov;
    public float velRot = 0.1f;
    public static Animator anim;
    public float speed;
    float rotPlayer = 0.3f;
    public Camera cam;
    public float verticalVel;
    public Vector3 movVector;
    playerInDoor playerInDoor;
    //[SerializeField] Transform sword;
    float time = 1f;
    //"Combo"
    //public float cooldownTime = 1.5f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 0.25f;

    //
    public static GameObject enemy;
    [SerializeField] GameObject[] objs;
    [SerializeField] GameObject[] weapons;
    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip[] audioC;
    GameObject trail;
    WaitForSeconds trailTimeOff = new WaitForSeconds(0.1f);
    [SerializeField] Collider[] colliders;
    [SerializeField] BoxCollider[] handsCol;
    public float staminaCost = 0.25f;
    public UnityEngine.UI.Image healthBar;
    float gravity = 1.2f;
    CharacterController controller;
    [SerializeField] Vector3 velocity;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }
    private void Start()
    {
        Cursor.visible = false;
        interaction = false;
        Died = false;
        audioS = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        //useGUILayout = false;
    }
    // Update is called once per frame
    void Update()
    {
        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2f) * gravity;
        if (controller.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }
        controller.Move(velocity);
        if (interaction == false)
        {
            InputMagnitude();
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                Combo_Anim();
            }
            if (Save.weaponChange)
            {
                Save.weaponChange = false;
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                }
                weapons[Save.weaponChoice].SetActive(true);
            }
            if (playerInDoor != null && Input.GetKeyDown(KeyCode.F) && !playerInDoor.CompareTag("chest"))
            {
                playerInDoor.open = !playerInDoor.open;
                playerInDoor.anim.SetBool("Open", playerInDoor.open);
            }
            time -= Time.deltaTime;
            if (objs[0].activeSelf == true&& Save.invisible == true)
            {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        objs[i].SetActive(false);
                    }
            }
            if (objs[0].activeSelf == false&& Save.invisible == false)
            {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        objs[i].SetActive(true);
                    }

            }

        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetFloat("InputMagnitude", 0);
        }

    }
    void MovePlayerRot()
    {
        Quaternion roti = new Quaternion(0, 0, 0, 0);
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        Vector3 front = cam.transform.forward;
        Vector3 right = cam.transform.right;
        front.Normalize();
        right.Normalize();

        dirMov = front * InputZ + right * InputX;
        roti = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirMov), velRot);
        transform.rotation = new Quaternion(0, roti.y, 0, roti.w);

    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        anim.SetFloat("Z", InputZ, 0, Time.deltaTime * 2);
        anim.SetFloat("X", InputX, 0, Time.deltaTime * 2);

        speed = new Vector2(InputX, InputZ).sqrMagnitude;
        if (speed > rotPlayer)
        {
            anim.SetFloat("InputMagnitude", speed, 0.01f, Time.deltaTime);
            MovePlayerRot();
        }
        else
        {
            anim.SetFloat("InputMagnitude", speed, 0.01f, Time.deltaTime);
        }
        if(InputX!=0 && Input.GetKey(KeyCode.LeftShift)|| InputZ != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        if (Input.GetButtonDown("Jump") && time<=0 && Save.staminaAmount > staminaCost)
        {
            Save.staminaAmount -= (staminaCost- (Save.staminaPower /10));
            anim.SetTrigger("jump");
            time = 1f;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("crouching", true);
        }
        else if(Input.GetKeyUp(KeyCode.C))
        {
            anim.SetBool("crouching", false);
        }
    }
    private void Combo_Anim()
    {

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle 0")|| anim.GetCurrentAnimatorStateInfo(0).IsName("Run") ||anim.GetCurrentAnimatorStateInfo(0).IsName("Run 1")|| anim.GetCurrentAnimatorStateInfo(0).IsName("Run 0")|| anim.GetCurrentAnimatorStateInfo(0).IsName("ShortSword")|| anim.GetCurrentAnimatorStateInfo(0).IsName("LongSword"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1")|| anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run") || anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run 0") || anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run 1")||anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1 0")|| anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1 1"))
        {
            anim.SetBool("hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2")|| anim.GetCurrentAnimatorStateInfo(0).normalizedTime>0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run2") || anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run2 0")||anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run2 1")|| anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2 0") || anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2 1"))
        {
            anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3") || anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3 0")|| anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3 1"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {

            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }
    void OnClick()
    {
        if (Time.timeScale != 0) 
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            speed = 0;
            if (noOfClicks == 1)
            {
                anim.SetBool("hit1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

            if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1") || noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run") || noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run 0") || noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit_Run 1") || noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1 0")|| noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1 1"))
            {
                anim.SetBool("hit1", false);
                anim.SetBool("hit2", true);
            }
            if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2")|| noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2 0")|| noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2 1"))
            {
                anim.SetBool("hit2", false);
                anim.SetBool("hit3", true);
            }
        }

    }
    void ColliderOn()
    {
        colliders[Save.weaponChoice].enabled = true;
    }
    void ColliderOff()
    {
        colliders[Save.weaponChoice].enabled = false;
    }

    void HandOn(int i)
    {
        switch (i)
        {
            case 0:
                handsCol[0].enabled = true;
                break;
            case 1:
                handsCol[0].enabled = false;
                break;
            case 2:
                handsCol[1].enabled = true;
                break;
            case 3:
                handsCol[1].enabled = false;
                break;
        }
    }
    void PlaySound(int sou)
    {
        audioS.PlayOneShot(audioC[sou]);
    }
    public void Damage(float amount)
    {
        healthBar.fillAmount -= amount;
    }
    public void Heal(float amount)
    {
        healthBar.fillAmount += amount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerInDoor = other.GetComponent<playerInDoor>();
        }
        if(AudioManager.instance.inBattle == false)
        {
            if (other.gameObject.CompareTag("Tavern"))
            {
                AudioManager.instance.Music(1, true);
                if (Open_Inventory.instance.objectives[2] == false)
                {
                    Open_Inventory.instance.UpdateMessage(true, 2, 20);
                }
            }
            if (other.gameObject.CompareTag("wizard"))
            {
                AudioManager.instance.Music(3, true);
                if (Open_Inventory.instance.objectives[0] == false)
                {
                    Open_Inventory.instance.UpdateMessage(true, 0, 20);
                }
            }
            if (other.gameObject.CompareTag("smith"))
            {
                AudioManager.instance.Music(4, true);
                if (Open_Inventory.instance.objectives[1] == false)
                {
                    Open_Inventory.instance.UpdateMessage(true, 1, 20);
                }
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            playerInDoor = null;
        }
        if (other.gameObject.CompareTag("Tavern")|| other.gameObject.CompareTag("wizard")|| other.gameObject.CompareTag("smith"))
        {
            AudioManager.instance.Music(0, true);
        }
    }
    /*private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.8f);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, sword.position);
    }*/
}
