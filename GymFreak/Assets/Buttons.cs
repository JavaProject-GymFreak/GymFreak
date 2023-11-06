using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    GameData gameData;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
    }

    public void ChangeSpeed()
    {
        gameData.speedLevel *= 2;
        if(gameData.speedLevel > 8)
        {
            gameData.speedLevel = 1;
        }
    }
    public void Pause()
    {
        gameData.pause = true;
    }
    public void Resume()
    {
        gameData.pause = false;
    }
}
