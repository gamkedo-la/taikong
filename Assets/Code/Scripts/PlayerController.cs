using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player controller design influenced by Mix and Jam
// https://www.youtube.com/watch?v=JVbr7osMYTo
public class PlayerController : MonoBehaviour {
    public float movementSpeed;
    private Transform shipModel;
    private Transform playerCamera;

    void Start() {
        shipModel = this.transform.GetChild(0);
        playerCamera = this.transform.GetChild(1);
    }

    void Update() {
        float x = 0;
        float y = 0;

        if (Input.GetKey("w")) {
            y = 1;
        }

        if (Input.GetKey("a")) {
            x = -1;
        }

        if (Input.GetKey("s")) {
            y = -1;
        }

        if (Input.GetKey("d")) {
            x = 1;
        }

        LocalMove(x, y, movementSpeed);
        ClampPosition();
    }

    void LocalMove(float x, float y, float speed) {
        // manage player vertical and horizontal movement
        shipModel.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        
        // Move ship forward in space
        // this.transform.localPosition += new Vector3(0, 0, speed / 6) * speed * Time.deltaTime;
    }

    void ClampPosition() {
        // Don't allow the player to move beyond the limits of the camera
        Vector3 pos = shipModel.localPosition;
        pos.x = Mathf.Clamp(pos.x, -9.0f, 9.0f);
        pos.y = Mathf.Clamp(pos.y, -5.0f, 5.0f);
        shipModel.localPosition = pos;
    }
}
