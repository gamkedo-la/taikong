using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth playerHealth = new UnitHealth(100, 100);

    public enum ControlScheme { game_pad, mouse_keys};
    public ControlScheme activeControlScheme;

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

    private void Start() {
        // Default to mouse and keys as pad is weird atm
        activeControlScheme = ControlScheme.mouse_keys;
    }
}
