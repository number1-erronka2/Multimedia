using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private string userName;
    private float puntuazioa;

    public User(){}
    public User(string userName, float puntuazioa){
        this.userName = userName;
        this.puntuazioa = puntuazioa;
    }

    public void setUserName(string userName){
        this.userName = userName;
    }

    public string getUserName(){
        return userName;
    }

    public void setPuntuazioa(float puntuazioa){
        this.puntuazioa = puntuazioa;
    }

    public float getPuntuazioa(){
        return puntuazioa;
    }
}
