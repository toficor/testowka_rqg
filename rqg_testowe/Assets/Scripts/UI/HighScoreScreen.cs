using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class HighScoreScreen : BaseUIScreen
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private List<HighscoreRecord> highscoreRecords = new List<HighscoreRecord>();

    private SaveManager.Highscores highscores;

    private const string scoreFormat = "score: {0}";
    private const string dateFormat = "date: {0}";

    public override void EnableScreen()
    {
        highscores = saveManager.Load();

        for(int i = 0; i < highscoreRecords.Count; i++)
        {
            if(i >= highscores.highscoreEntries.Count)
            {
                break;
            }

            highscoreRecords[i].data.text = string.Format(dateFormat, highscores.highscoreEntries[i].dateTime);
            highscoreRecords[i].score.text = string.Format(scoreFormat, highscores.highscoreEntries[i].score.ToString());
            highscoreRecords[i].gameObject.SetActive(true);
        }

        base.EnableScreen();
    }


}


