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

    private GameObject shipModel;
    private GameObject playerCamera;
    private GameObject crosshair;
    
    public GameObject laserPrefab;
    public float movementSpeed;
    public bool isBoosting = false;
    public Vector3 crosshairWorldPos;
    public float crosshairSpeed;

    void Start() {
        shipModel = GameObject.Find("Ship");
        playerCamera = GameObject.Find("Camera");
        crosshair = GameObject.Find("Crosshair");
    }

    void Update() {
        RegisterInputs();
        ClampPlayerPosition();
        ClampCrosshairPosition();
        UpdateCameraPosition();
        PointShipAtCrosshair();
    }

    void RegisterInputs() {
        float ship_x = 0;
        float ship_y = 0;
        float cross_x = 0;
        float cross_y = 0;

        // WASD keyboard input for movement
        if (Input.GetKey("w")) {
            ship_y = 1;
        }

        if (Input.GetKey("a")) {
            ship_x = -1;
        }

        if (Input.GetKey("s")) {
            ship_y = -1;
        }

        if (Input.GetKey("d")) {
            ship_x = 1;
        }

        // Simple console controller input
        ship_x = Input.GetAxis("Horizontal");
        ship_y = Input.GetAxis("Vertical");

        // Crosshair movement with the right stick
        cross_x = Input.GetAxis("HorizontalRIghtStick");
        cross_y = Input.GetAxis("VerticalRightStick");

        // if (Input.GetKeyDown("left shift")) {
        //     isBoosting = true;
        // }

        // if (Input.GetKeyUp("left shift")) {
        //     isBoosting = false;
        // }

        isBoosting = Input.GetAxis("Boost") == 1;

        if (Input.GetKeyDown("space")) {
            FireLasers();
        }

        if (Input.GetAxis("Fire1") == 1) {
            FireLasers();
        }

        ShipMove(ship_x, ship_y, movementSpeed);
        CrosshairMove(cross_x, cross_y, crosshairSpeed);
    }

    void ShipMove(float x, float y, float speed) {
        // manage player vertical and horizontal movement
        shipModel.transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    void ClampPlayerPosition() {
        // Don't allow the player to move beyond the limits of the camera
        Vector3 pos = shipModel.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -9.0f, 9.0f);
        pos.y = Mathf.Clamp(pos.y, -5.0f, 5.0f);
        shipModel.transform.localPosition = pos;
    }

    void CrosshairMove(float x, float y, float speed) {
        crosshair.transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    void ClampCrosshairPosition() {
        // Don't allow the crosshair to move beyond the limits of the camera
        Vector3 pos = crosshair.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -500.0f, 500.0f);
        pos.y = Mathf.Clamp(pos.y, -285.0f, 285.0f);
        crosshair.transform.localPosition = pos;
    }

    void UpdateCameraPosition() {
	    Vector3 currentPos = playerCamera.transform.localPosition;
	    float cameraPosDifference = NORMAL_CAM.z - BOOST_CAM.z;
	    
        if (isBoosting) {
	        currentPos = BOOST_CAM;
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = BOOST_SPEED;
        } else {
	        currentPos = NORMAL_CAM;
            this.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = NORMAL_SPEED;            
        }

        playerCamera.transform.localPosition = currentPos;
    }

    void PointShipAtCrosshair() {
        crosshairWorldPos = playerCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(crosshair.transform.position.x, crosshair.transform.position.y, 1500f));
        shipModel.transform.LookAt(crosshairWorldPos);
    }

    void FireLasers() {
        GameObject laserShot = Instantiate(laserPrefab, shipModel.transform.position, Quaternion.LookRotation(crosshairWorldPos));
    }
}
