using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HighScore 
{
    public int score {get; set;}

    public string name {get; set;}

    public int date {get; set;}

    public int id {get; set;}

    public HighScore(int id, string name, int score,  int date )
    {
        this.id = id;
        this.score = score;
        this.name = name;
        this.date = date;
    }

}
