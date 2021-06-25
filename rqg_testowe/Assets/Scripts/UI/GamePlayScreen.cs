using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayScreen : BaseUIScreen
{
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI wavetext;
    [SerializeField] private TextMeshProUGUI lifeText;


    private const string scoreFormat = "current score: {0}";
    private const string waveFormat = "current wave: {0}";
    private const string lifeFormat = "lives left: {0}";


    public override void DoJob()
    {
        ManageText();
    }

    private void ManageText()
    {
        scoreText.text = string.Format(scoreFormat, gameManagerData.score);
        wavetext.text = string.Format(waveFormat, gameManagerData.currentWave);
        lifeText.text = string.Format(lifeFormat, playerData.lives);
    }

}
