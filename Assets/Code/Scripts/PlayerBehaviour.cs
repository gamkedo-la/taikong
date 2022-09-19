using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : Health
{
    void Update()
    {
        //debugging
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeHealth(-20, gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeHealth(20, gameObject);
        }
    }
}
