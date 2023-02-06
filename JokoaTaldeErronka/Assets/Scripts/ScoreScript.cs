using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI puntuacion;
    public static int ScoreNumber = 0;
    public static ScoreScript instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ScoreNumber = 0;
        puntuacion.text = "Score: " + ScoreNumber;
        PlayerPrefs.SetInt("Puntuazioa", 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "moneda")
        {
            ScoreNumber += 7;
            puntuacion.text = "Score: " + ScoreNumber;
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("Puntuazioa", ScoreNumber);
        }
    }
    public void suma()
    {
        ScoreNumber += 20; 
        puntuacion.text = "Score: " + ScoreNumber;
        PlayerPrefs.SetInt("Puntuazioa", ScoreNumber);
    }

    
    
}
