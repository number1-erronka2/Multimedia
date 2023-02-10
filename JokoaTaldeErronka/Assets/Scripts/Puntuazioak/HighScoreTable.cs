using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    public GameObject Player;
    private bool active = false;

    public HighScoreTable(){}


    void Update(){
        entryContainer = transform.Find("ScoresContainer");
        entryTemplate = entryContainer.Find("ScoreTemplate");
        entryTemplate.gameObject.SetActive(false);
        if (Player == null && active == false) {
            showHighScores();
            active = true;
        }
    }

    public void showHighScores(){
        DBDemo dbDemo = new DBDemo();
        List<HighScore> highScores = dbDemo.GetPartidak();
        for (int i = 0; i < 5; i++){
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -50f * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("Rank").GetComponent<Text>().text = "#" + (i + 1).ToString();
            entryTransform.Find("Name").GetComponent<Text>().text = highScores[i].name;
            entryTransform.Find("Score").GetComponent<Text>().text = highScores[i].score.ToString();
        }
    }
}
