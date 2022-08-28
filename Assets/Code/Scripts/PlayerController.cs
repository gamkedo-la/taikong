using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player controller design influenced by Mix and Jam
// https://www.youtube.com/watch?v=JVbr7osMYTo
public class PlayerController : MonoBehaviour {
    private float NORMAL_SPEED = 20;
    private float BOOST_SPEED = 30;

    private Vector3 BOOST_CAM = new Vector3(0, 0, -15);
    private Vector3 NORMAL_CAM = new Vector3(0, 0, -10);

    private Transform shipModel;
    private Transform playerCamera;
    
    public GameObject laserPrefab;
    public float movementSpeed;
    public bool isBoosting = false;

    void Start() {
        shipModel = this.transform.Find("Ship");
        playerCamera = this.transform.Find("Camera");
    }

    void Update() {
        RegisterInputs();
        ClampPlayerPosition();
        UpdateCameraPosition();
    }

    void RegisterInputs() {
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

        if (Input.GetKeyDown("left shift")) {
            isBoosting = true;
        }

        if (Input.GetKeyUp("left shift")) {
            isBoosting = false;
        }

        if (Input.GetKeyDown("space")) {
            FireLasers();
        }

        LocalMove(x, y, movementSpeed);
    }

    void LocalMove(float x, float y, float speed) {
        // manage player vertical and horizontal movement
        shipModel.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        
        // Move ship forward in space
        // this.transform.localPosition += new Vector3(0, 0, speed / 6) * speed * Time.deltaTime;
    }

    void ClampPlayerPosition() {
        // Don't allow the player to move beyond the limits of the camera
        Vector3 pos = shipModel.localPosition;
        pos.x = Mathf.Clamp(pos.x, -9.0f, 9.0f);
        pos.y = Mathf.Clamp(pos.y, -5.0f, 5.0f);
        shipModel.localPosition = pos;
    }

    void UpdateCameraPosition() {
	    Vector3 currentPos = playerCamera.localPosition;
	    float cameraPosDifference = NORMAL_CAM.z - BOOST_CAM.z;
	    
        if (isBoosting) {
	        currentPos = BOOST_CAM;
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = BOOST_SPEED;
        } else {
	        currentPos = NORMAL_CAM;
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = NORMAL_SPEED;            
        }

        playerCamera.localPosition = currentPos;
    }

    void FireLasers() {
        GameObject laserShot = Instantiate(laserPrefab, shipModel.position, this.transform.rotation);
    }
}
