using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    string userName;
    public TextMeshProUGUI text;
    public TMP_InputField  usuarioa;
    public MainMenu(){}

    public void startGame(){
        SceneManager.LoadScene("Game");
        Debug.Log("Game is exiting");
    }

    public void exitGame(){
    //para que salga tambien si estas en unityGame
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    
    public void setUserName(string inputUserName){
        userName = inputUserName;
        PlayerPrefs.SetString("userName", userName);
    }

    public void home(){
        DBDemo dbDemo = new DBDemo();
        Debug.Log("DBDemo sortzen");
        dbDemo.sortuDB();
        Debug.Log("DBDemo sortuta");



        if(dbDemo.VerifyUser(userName)){
            SceneManager.LoadScene("MainMenu");
            text.SetText("");
        } else {
            //visualizar mensaje de error
            text.color = Color.red;
            text.SetText("Izen okerra. Saiatu berriz");
            usuarioa.SetTextWithoutNotify("");
            
        }
    }
}
