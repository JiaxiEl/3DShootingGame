using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Transform ItemTrans;

    protected ItemSpawn itemspawn;
    // Start is called before the first frame update
    void Start()
    {
        ItemTrans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(ItemSpawn spawn)
    {
        itemspawn = spawn;
        itemspawn.ItemNumber++;
    }
}
