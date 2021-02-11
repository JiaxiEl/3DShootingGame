using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    
    public Transform enemyTrans;
    private Transform SpawnPointTrans;
    public int EnemyNumber = 0;
    public int Max_EnemyNumber = 3;
    public float CD_EnemySpawn = 0;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPointTrans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Stop if EnemyNumber is lager than Max_EnemyNumber
        if (EnemyNumber >= Max_EnemyNumber)
            return;

        //Enemy Spawns
        CD_EnemySpawn -= Time.deltaTime;
        if (CD_EnemySpawn <= 0)
        {
            CD_EnemySpawn = Random.value * 15f;
            if (CD_EnemySpawn < 5)
                CD_EnemySpawn = 5;
            Transform EnemyObj = Instantiate(enemyTrans, SpawnPointTrans.position, Quaternion.identity);
            player.enemy.Add(EnemyObj);
            Enemy enemy = EnemyObj.GetComponent<Enemy>();
            enemy.Init(this);
            

        }
    }
}
