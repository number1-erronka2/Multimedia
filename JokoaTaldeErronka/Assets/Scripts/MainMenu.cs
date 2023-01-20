using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string userName;

    public MainMenu(){}

    public void startGame(){
        SceneManager.LoadScene("Game");
        Debug.Log("Game is exiting");
    }

    public void exitGame(){
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    
    public void setUserName(string inputUserName){
        userName = inputUserName;
        PlayerPrefs.SetString("userName", userName);
    }

    public void home(){
        DBDemo dbDemo = new DBDemo();
        if(dbDemo.VerifyUser(userName)){
            SceneManager.LoadScene("MainMenu");
        } else {
            //visualizar mensaje de error
        }
    }
}
