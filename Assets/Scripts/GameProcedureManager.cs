using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProcedureManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        GameObject mainPlayer = GameObject.FindWithTag("Player");
        if(mainPlayer)
        {
            MainPlayerComp mainPlayerComp = mainPlayer.GetComponent<MainPlayerComp>();
            mainPlayerComp.GameOverEvent.AddListener(OnGameOver);
            mainPlayerComp.GameStartEvent.AddListener(OnGameStart);
        }
        else
        {
            Debug.LogError("GameProcedureManager: Cannot Find MainPlayer.");
        }
        
    }
    void OnGameStart(PlayerScore playerScore)
    {
        Debug.Log("GameOverManager: OnGameStart");
        StaticScoreRecorder.playerScore = playerScore;
    }

    void OnGameOver(PlayerScore playerScore)
    {
        Debug.Log("GameOverManager: OnGameOver");
        StaticScoreRecorder.playerScore = playerScore;

        SceneManager.LoadScene("Scenes/GameOverScene", LoadSceneMode.Single);
    }
}
