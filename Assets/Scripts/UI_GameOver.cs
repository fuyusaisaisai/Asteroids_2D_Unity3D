using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    public Text NumAsteroidText;
    public Button RestartButton;

    // Start is called before the first frame update
    void Start()
    {
        if(NumAsteroidText)
        {
            NumAsteroidText.text = StaticScoreRecorder.playerScore.numAsteroids.ToString();
        }

        if(RestartButton)
        {
            RestartButton.onClick.AddListener(OnRestartClicked);
        }

    }

    void OnRestartClicked()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

}
