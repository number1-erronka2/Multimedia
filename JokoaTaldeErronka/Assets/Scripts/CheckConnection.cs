using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheckConnection : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.google.com");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log("Sartu da");

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}
