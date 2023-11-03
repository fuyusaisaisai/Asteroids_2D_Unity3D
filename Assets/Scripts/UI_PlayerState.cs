using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerState : MonoBehaviour
{
    public Text numAsteroidText;

    public Text numLifeText;

    public void SetNumAsteroid(int numAsteroid)
    {
        if(numAsteroidText)
        {
            numAsteroidText.text = numAsteroid.ToString();
        }
        
    }

    public void SetNumLife(int numLife)
    {
        if(numLifeText)
        {
            numLifeText.text = numLife.ToString();
        }
        
    }
}
