using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Texto : MonoBehaviour
{
    public TextMeshProUGUI canvasText1;
    void Start()
    {
        canvasText1.color = Color.cyan;
        Invoke("DisableText", 4f);//invoke after 5 seconds
    }
    void DisableText()
    {
        canvasText1.enabled = false;

    }
}


