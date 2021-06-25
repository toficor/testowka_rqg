using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SaveMenager", menuName = "Managers/SaveManager")]
public class SaveManager : ScriptableObject
{  

    public Highscores highscores;

    public void Save(HighscoreEntry highscoreEntry)
    {
        string jsonString = PlayerPrefs.GetString("highscores");
        if (!string.IsNullOrEmpty(jsonString))
        {
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
            if(highscores.highscoreEntries.Count == 10)
            {
                HighscoreEntry lowestEntry = highscores.highscoreEntries[0];

                for(int i = 0; i < highscores.highscoreEntries.Count; i++)
                {
                    if(highscores.highscoreEntries[i].score < lowestEntry.score)
                    {
                        lowestEntry = highscores.highscoreEntries[i];
                    }
                }

                if(lowestEntry.score >= highscoreEntry.score)
                {
                    return;
                }

                highscores.highscoreEntries.Remove(lowestEntry);
            }
            highscores.highscoreEntries.Add(highscoreEntry);
            jsonString = JsonUtility.ToJson(highscores);

            Debug.LogError(jsonString);

            PlayerPrefs.SetString("highscores", jsonString);
            PlayerPrefs.Save();
        }
        else
        {
            Highscores highscores = new Highscores();
            highscores.highscoreEntries.Add(highscoreEntry);
            jsonString = JsonUtility.ToJson(highscores);

            Debug.LogError(jsonString);

            PlayerPrefs.SetString("highscores", jsonString);
            PlayerPrefs.Save();

        }
    }


    public Highscores Load()
    {
        string scores = PlayerPrefs.GetString("highscores");
        Highscores highscores = (Highscores)JsonUtility.FromJson(scores, typeof(Highscores));
        return highscores;
    }

    [Serializable]
    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntries = new List<HighscoreEntry>(10);
    }




}

[Serializable]
public struct HighscoreEntry
{
    public int score;
    public string dateTime;
}