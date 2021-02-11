using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public Transform Item;
    private Transform SpawnPointTrans;
    public int ItemNumber = 0;
    public float CD_ItemSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPointTrans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Stop if EnemyNumber is lager than Max_EnemyNumber
        if (ItemNumber >= 1)
            return;

        //Enemy Spawns
        CD_ItemSpawn -= Time.deltaTime;
        if (CD_ItemSpawn <= 0)
        {
            CD_ItemSpawn = Random.value * 15f;
            if (CD_ItemSpawn < 8)
                CD_ItemSpawn = 8;
            Transform itemObj = Instantiate(Item, SpawnPointTrans.position, Quaternion.identity);

            Item item = itemObj.GetComponent<Item>();
            item.Init(this);


        }
    }
}
