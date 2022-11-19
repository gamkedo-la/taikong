using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

    public GameObject player;
    public bool isVisible;

    void Start() 
    {

    }

    void OnGUI()
    {
        if (player.scene.IsValid() && isVisible) {
            GUI.Label(new Rect(10, 10, 500, 20), "Player X: " + player.transform.GetChild(0).localPosition.x);
            GUI.Label(new Rect(10, 25, 500, 20), "Player Y: " + player.transform.GetChild(0).localPosition.y);
            GUI.Label(new Rect(10, 40, 500, 20), "Speed: " + player.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed);
            GUI.Label(new Rect(10, 55, 500, 20), "Boost? : " + player.GetComponent<PlayerInputs>().shipBoosting);
            GUI.Label(new Rect(10, 70, 500, 20), "Crosshair X: " + player.transform.GetChild(2).transform.GetChild(0).position.x);
            GUI.Label(new Rect(10, 85, 500, 20), "Crosshair Y: " + player.transform.GetChild(2).transform.GetChild(0).position.y);
            GUI.Label(new Rect(10, 100, 500, 20), "Crosshair Pos: " + player.GetComponent<PlayerInputs>().crosshairWorldPos);
        }
    }
}
