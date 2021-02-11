using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestShow : MonoBehaviour
{
    public GameObject quest;
    public Text questOne;
    public Text questTwo;
    public Player player;
    public int EnemyCount = 0;
    public int EnemyRequirment = 20;
    public int Score = 0;
    public int ScoreRequirment = 1000;

    // Start is called before the first frame update
    void Start()
    {
        Score = player.PlayerScore;
        quest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            quest.SetActive(true);
            questOne.text = "Kill " + EnemyCount.ToString() + " / " + EnemyRequirment.ToString() + ", get Random items";
            questTwo.text = "Meet " + Score.ToString() + " / " + ScoreRequirment.ToString() + ", get Random items";
            
        }
        else if (Input.GetKeyUp(KeyCode.Tab)){
            quest.SetActive(false);
        }
        Score = player.PlayerScore;
        CheckComplete();
    }

    void CheckComplete()
    {
        if (EnemyCount >= EnemyRequirment)
        {
            player.NumberHp_Potioin += Random.Range(0, 3);
            player.NumberSpeed_Potioin += Random.Range(0, 3);
            EnemyCount = 0;
            EnemyRequirment = Random.Range(20, 100);
        }

        if (Score >= ScoreRequirment)
        {
            player.NumberHp_Potioin += Random.Range(1, 3);
            player.NumberSpeed_Potioin += Random.Range(1, 3);
            ScoreRequirment += Random.Range(500, 2000);
        }
    }

}
