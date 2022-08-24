using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player controller design influenced by Mix and Jam
// https://www.youtube.com/watch?v=JVbr7osMYTo
public class PlayerController : MonoBehaviour {
    public float movementSpeed;
    public GameObject shipModel;
    public GameObject playerCamera;

    void Update() {
        float x = 0;
        float y = 0;

        if (Input.GetKey("w")) {
            y += 1;
        }

        if (Input.GetKey("a")) {
            x -= 1;
        }

        if (Input.GetKey("s")) {
            y -= 1;
        }

        if (Input.GetKey("d")) {
            x += 1;
        }

        LocalMove(x, y, movementSpeed);
        // ClampPosition();
    }

    void LocalMove(float x, float y, float speed) {
        shipModel.transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        this.transform.localPosition += new Vector3(0, 0, speed / 4) * speed * Time.deltaTime;
    }

    void ClampPosition() {
        Vector3 pos = shipModel.transform.localPosition;
        pos.x = Mathf.Clamp01(5);
        pos.y = Mathf.Clamp01(5);
        shipModel.transform.position = pos;
    }
}
