using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameResultScreen : BaseUIScreen
{
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;

    private const string scoreFormat = "score gained: {0}";
    private const string waveFormat = "wave passed: {0}";

    public override void DoJob()
    {
        ManageTexts();
    }


    private void ManageTexts()
    {
        scoreText.text = string.Format(scoreFormat, gameManagerData.score);
        waveText.text = string.Format(waveFormat, gameManagerData.wavePassed);
    }
}
