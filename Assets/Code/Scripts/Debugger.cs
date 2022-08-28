using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

    public GameObject playerObject;

    void OnGUI()
    {
        if (playerObject.scene.IsValid()) {
            GUI.Label(new Rect(10, 10, 500, 20), "Player X: " + playerObject.transform.GetChild(0).localPosition.x);
            GUI.Label(new Rect(10, 25, 500, 20), "Player Y: " + playerObject.transform.GetChild(0).localPosition.y);
            GUI.Label(new Rect(10, 40, 500, 20), "Speed: " + playerObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed);
            GUI.Label(new Rect(10, 55, 500, 20), "Boast? : " + playerObject.GetComponent<PlayerController>().isBoosting);
            
        }
    }
}
