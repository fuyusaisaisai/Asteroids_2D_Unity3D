using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI_Start : MonoBehaviour
{
    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        if (StartButton)
        {
            StartButton.onClick.AddListener(OnStartClicked);
        }

    }


    void OnStartClicked()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }

}
