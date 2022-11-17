using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerInputs : MonoBehaviour
{
    [Header("Player Weapon Prefabs")]
    [SerializeField] GameObject laserPrefab;

    [Header("Player Sound Effects")]
    [SerializeField] AudioSource laserSfx;
    
    [Header("Game settings")]
    [SerializeField] float shipSpeed;
    [SerializeField] float aimSpeed;
    [SerializeField] float flightSpeedNormal;
    [SerializeField] float flightSpeedBoost;
    [SerializeField] float boostFuel;
    [SerializeField] float boostBurnRate;

    [Header("Camera Positions")]
    [SerializeField] float normalCameraPosition;
    [SerializeField] float boostCameraPosition;
    [SerializeField] float cameraPanSpeed;

    private Transform shipModel;
    private Transform laserL;
    private Transform laserR;
    private Camera shipCamera;
    private Transform crosshair;
    private Vector2 movementDirection;
    private Vector2 aimingDirection;
    private Vector3 crosshairWorldPos;
    private bool shipBoosting;


    void Start() {
        shipBoosting = false;
        shipModel = GameObject.Find("Ship").transform;
        shipCamera = GameObject.Find("Camera").GetComponent<Camera>();
        laserL = GameObject.Find("Laser_L").transform;
        laserR = GameObject.Find("Laser_R").transform;
        crosshair = GameObject.Find("Crosshair").transform;
    }

    void FixedUpdate() 
    {
        ShipMovement();
        CrosshairMovement();
        PointShipAtCrosshair();
        SetCameraPosition();
    }

    void ShipMovement() {
        shipModel.localPosition += new Vector3(movementDirection.x, movementDirection.y, 0) * shipSpeed * Time.fixedDeltaTime;

        // Don't allow the player to move beyond the limits of the camera
        Vector3 pos = shipModel.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -11.0f, 11.0f);
        pos.y = Mathf.Clamp(pos.y, -8.5f, 8.0f);
        shipModel.transform.localPosition = pos;
    }

    void CrosshairMovement() {
        crosshair.localPosition += new Vector3(aimingDirection.x, aimingDirection.y, 0) * aimSpeed * Time.fixedDeltaTime;

        // Don't allow the crosshair to move beyond the limits of the camera
        Vector3 pos = crosshair.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -500.0f, 500.0f);
        pos.y = Mathf.Clamp(pos.y, -285.0f, 285.0f);
        crosshair.transform.localPosition = pos;
    }

    void PointShipAtCrosshair() {
        crosshairWorldPos = shipCamera.ScreenToWorldPoint(new Vector3(crosshair.transform.position.x, crosshair.transform.position.y, 500f));
        shipModel.transform.LookAt(crosshairWorldPos);
    }

    void SetCameraPosition() {
        float currentCameraPosition = shipCamera.transform.localPosition.z;

        if (shipBoosting && boostFuel > 0) {
            boostFuel -= Time.fixedDeltaTime * boostBurnRate;
            // Debug.Log(boostFuel);
        } else {
            shipBoosting = false;
        }

        if (shipBoosting && currentCameraPosition >= boostCameraPosition) {
            currentCameraPosition -= Time.fixedDeltaTime * cameraPanSpeed;
        } else if (currentCameraPosition <= normalCameraPosition) {
            currentCameraPosition += Time.fixedDeltaTime * cameraPanSpeed * 1.5f;
        }

        shipCamera.transform.localPosition = new Vector3(0, 0, currentCameraPosition);
    }

    public void OnMovement(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
    }

    public void OnAim(InputValue value)
    {
        aimingDirection = value.Get<Vector2>();
    }
    
    public void OnFire(InputValue value)
    {
        RaycastHit hit;
        
        if (Physics.Raycast(shipModel.position, shipModel.TransformDirection(Vector3.forward), out hit)) {
            Instantiate(laserPrefab, laserL.position, Quaternion.LookRotation(hit.point - shipModel.transform.position));
            Instantiate(laserPrefab, laserR.position, Quaternion.LookRotation(hit.point - shipModel.transform.position));
            laserSfx.Play();
        }
    }

    public void OnBoost(InputValue value)
    {
        if (!shipBoosting && boostFuel > 0)
            shipBoosting = true;
    }

    public void OnPause(InputValue value)
    {
        Debug.Log("Pause!");
    }
}
