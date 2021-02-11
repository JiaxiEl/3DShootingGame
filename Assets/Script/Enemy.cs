using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{

    public static float EnemyDamage = 10.0f;
    Transform enemyTrans;
    Player player;
    public float enemySpeed=0.5f;
    public float enemyRotate=30f;
    float timerforEnemy = 2;
    public float enemyLife = 15.0f;
    protected EnemySpawn enemyspawn;
    Animator EnemyAnimator;
    UnityEngine.AI.NavMeshAgent EnemyNav;
    public AudioSource attackAudio, walkAudio, deathAudio, onDamage;
    public static int scorecount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        
        enemyTrans = this.transform;
        EnemyAnimator = this.GetComponent<Animator>();
        EnemyNav = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();


    }



    public void EnemyMove()
    {
        if (player.PlayerLife <= 0)
        {
            return;
        }
        //Idle
        AnimatorStateInfo stateInfo = EnemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.anim_idle") && !EnemyAnimator.IsInTransition(0))
        {
            EnemyAnimator.SetBool("anim_idle", false);
            timerforEnemy -= Time.deltaTime;
            if (timerforEnemy > 0)
                return;

            if (Vector3.Distance(enemyTrans.position, player.PlayerTrans.position) < 1.5f)
            {
                EnemyAnimator.SetBool("anim_attack", true);


            }
            else
            {
                timerforEnemy = 1;

                EnemyNav.SetDestination(player.PlayerTrans.position);

                EnemyAnimator.SetBool("anim_walk", true);
            }
        }

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.anim_walk") && !EnemyAnimator.IsInTransition(0))
        {
            walkAudio.Play();
            EnemyAnimator.SetBool("anim_walk", false);

            timerforEnemy -= Time.deltaTime;
            if (timerforEnemy < 0)
            {
                EnemyNav.SetDestination(player.PlayerTrans.position);

                timerforEnemy = 1;
            }
            if (Vector3.Distance(enemyTrans.position, player.PlayerTrans.position) <= 1.5f)
            {

                EnemyNav.ResetPath();

                EnemyAnimator.SetBool("anim_attack", true);
            }
        }
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.anim_attack") && !EnemyAnimator.IsInTransition(0))
        {

            RotateTo();
            attackAudio.Play();
            EnemyAnimator.SetBool("anim_attack", false);

            if (stateInfo.normalizedTime >= 1.0f)
            {

                EnemyAnimator.SetBool("anim_idle", true);

                timerforEnemy = 2;

                player.OnDamage(EnemyDamage);

            }
        }
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.anim_death") && !EnemyAnimator.IsInTransition(0))
        {
          
            if (stateInfo.normalizedTime >= 1.0f)
            {

                player.getPoints(100);
                enemyspawn.EnemyNumber--;
                Destroy(this.gameObject);
            }
        }
    }
    void RotateTo()
    {

        Vector3 PlayerDirection = player.PlayerTrans.position - enemyTrans.position;

        Vector3 EnemyDirection = Vector3.RotateTowards(transform.forward, PlayerDirection, enemyRotate * Time.deltaTime, 0.0f);

        enemyTrans.rotation = Quaternion.LookRotation(EnemyDirection);
    }
    void WalkTo()
    {
        float speed = enemySpeed * Time.deltaTime;
        EnemyNav.Move(enemyTrans.TransformDirection((new Vector3(0, 0, speed))));
    }
    public void OnDamage(float damage)
    {
       
        enemyLife -= damage;
        if(enemyLife > 0)
        {
            if (onDamage.isPlaying)
            {
                return;
            }
            else
            {

                onDamage.Play();
            }

        }
        if (enemyLife <= 0f)
        {
            if (scorecount == 1000)
            {
                EnemyDamage += 5;
                scorecount = 0;

            }
            scorecount += 100;
            EnemyAnimator.SetBool("anim_death", true);
            deathAudio.Play();
        }
    }
    public void Init(EnemySpawn spawn)
    {
        enemyspawn = spawn;
        enemyspawn.EnemyNumber++;
    }
}
