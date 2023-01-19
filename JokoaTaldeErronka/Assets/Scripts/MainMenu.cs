using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string userName;

    public void exitGame(){
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void getUserName(string inputUserName){
        userName = inputUserName;
        Debug.Log(inputUserName);
    }

    public void home(){
        DBDemo dbDemo = new DBDemo();
        Debug.Log(userName);
        if(dbDemo.VerifyUser(userName)){
            SceneManager.LoadScene("MainMenu");
        } else {
            //visualizar mensaje de error
        }
    }


    public void startGame(){
        SceneManager.LoadScene("Game");
        Debug.Log("Game is exiting");
    }
}
