using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    
    public Transform PlayerTrans;
    public int PlayerScore;
    public Image healthState;
    public Image SpeedUpState;
    
    public Text score;
    public Text numofhealthpotion;
    public Text numofspeedpotion;
    public float PlayerSpeed = 3.0f;
    float PlayerGravity = -9.8f;
    public CharacterController PlayerController;
    public float PlayerLife = 0f;
    public float Max_PlayerLife = 0f;
    Vector3 velocity;
    public List<Transform> enemy;

    public int NumberHp_Potioin=0;
    public int NumberSpeed_Potioin=0;

    public float TimeofSpeedUp = 0;
    float Max_SpeedUp = 10f;
    public float distance = 0.4f;
    public bool isSpeedUp = false;

    public QuestShow questSHOW;
    public LayerMask layerMask;
    bool isgroud;
    public Transform checkgroud;
    public AudioSource ItemGet, BGM;

    public GameObject gameover;
    public GameObject nofirstbadge;
    public GameObject firstbadge;
    public GameObject nosecbadge;
    public GameObject secondbadge;
    //public AudioClip ShootingAudio;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTrans = this.transform;
        Max_PlayerLife = 100f;
        PlayerLife = Max_PlayerLife;
        gameover.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        nofirstbadge.SetActive(true);
        firstbadge.SetActive(false);
        nosecbadge.SetActive(true);
        secondbadge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerLife <= 0)
        {
            return;
        }

        isgroud = Physics.CheckSphere(checkgroud.position, distance, layerMask);
        if (isgroud && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        PlayerMovement();
        healthState.transform.localScale = new Vector3(PlayerLife / Max_PlayerLife, healthState.transform.localScale.y, healthState.transform.localScale.z);
        SpeedUpState.transform.localScale = new Vector3(TimeofSpeedUp / Max_SpeedUp, healthState.transform.localScale.y, healthState.transform.localScale.z);
        score.text = "Score: " + PlayerScore.ToString();
        usePotion();
        numofspeedpotion.text = NumberSpeed_Potioin.ToString();
        numofhealthpotion.text = NumberHp_Potioin.ToString();
    }
    public void PlayerMovement()
    {        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        PlayerController.Move(move * PlayerSpeed * Time.deltaTime);
        velocity.y += PlayerGravity * Time.deltaTime;
    }

    public void getPoints(int points)
    {
        PlayerScore += points;
        questSHOW.EnemyCount++;
        if(PlayerScore >= 2000)
        {
            nosecbadge.SetActive(false);
            secondbadge.SetActive(true);
        }
    }

    public void OnDamage(float damage)
    {
        PlayerLife -= damage;
        if (PlayerLife <= 0) 
        {
            gameover.SetActive(true);
            Screen.lockCursor = false;

        }

    }

    void usePotion()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(NumberHp_Potioin >= 1)
            {
                NumberHp_Potioin -= 1;
                PlayerLife += 20;
                if (PlayerLife >= 100)
                    PlayerLife = 100;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (NumberSpeed_Potioin >= 1)
            {
                NumberSpeed_Potioin -= 1;
                TimeofSpeedUp = 10;
                isSpeedUp = true;
            }
        }
        if (isSpeedUp)
        {

            TimeofSpeedUp -= Time.deltaTime;
            PlayerSpeed = 6;
            if(TimeofSpeedUp < 0)
            {
                PlayerSpeed = 3;
                TimeofSpeedUp = 0;
                isSpeedUp = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hp_Potion")
        {
            ItemGet.Play();
            NumberHp_Potioin += 1;
            Destroy(other.gameObject);
            nofirstbadge.SetActive(false);
            firstbadge.SetActive(true);
        }
        if(other.tag == "Speed_Potion")
        {
            ItemGet.Play();
            NumberSpeed_Potioin += 1;
            Destroy(other.gameObject);
        }
    }
}

