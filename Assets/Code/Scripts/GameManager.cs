using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

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
