using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void startgame(string scenename)
    {
        SceneManager.LoadScene("HappyHaunting", LoadSceneMode.Single);
    }
    
    public void doquit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void tocredit(string scenename)
    {
        SceneManager.LoadScene("CreditScene", LoadSceneMode.Single);
    }
    public void tohowtoplay(string scenename)
    {
        SceneManager.LoadScene("HowToPlay", LoadSceneMode.Single);

    }
    public void tomain(string scenename)
    {
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}

