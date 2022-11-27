using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth playerHealth = new UnitHealth(100, 100);

    public enum GameState
    {
        mainmenu,
        playing,
        dying,
        paused,
        levelend,
        gameover
    }

    public static GameState currentState;

    private void Awake()
    {
        if(gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
}
