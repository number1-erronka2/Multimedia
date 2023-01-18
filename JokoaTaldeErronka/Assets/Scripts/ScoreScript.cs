using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI puntuacion;
    private int ScoreNumber;

    // Start is called before the first frame update
    void Start()
    {
        
        ScoreNumber = 0;
        puntuacion.text = "Score: " + ScoreNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "moneda")
        {
            ScoreNumber += 7;
            Destroy(collision.gameObject);
            puntuacion.text = "Score: " + ScoreNumber;
        }
    }
}
