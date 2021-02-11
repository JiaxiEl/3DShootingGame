using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeSystem : MonoBehaviour
{
    public GameObject badgemenu;
    // Start is called before the first frame update
    void Start()
    {
        badgemenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            badgemenu.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            badgemenu.SetActive(false);
        }

    }
}
