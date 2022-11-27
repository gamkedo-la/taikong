using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField] public bool shipBoosting;
    [SerializeField] float boostFuel;
    [SerializeField] float boostBurnRate;
    [SerializeField] GameObject boostTextBox;

    [Header("Camera Positions")]
    [SerializeField] float normalCameraPosition;
    [SerializeField] float boostCameraPosition;
    [SerializeField] float cameraPanSpeed;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;

    [Header("Next Level Menu")]
    [SerializeField] GameObject levelMenu;
    [SerializeField] GameObject scoreWrapper;

    [Header("Player Information")]
    public Vector3 crosshairWorldPos;

    private Transform shipModel;
    private Transform laserL;
    private Transform laserR;
    private Camera shipCamera;
    private Transform crosshair;
    private Vector2 movementDirection;
    private Vector2 aimingDirection;
    private float levelDistance;
    private float levelMenuOpacity = 0;

    


    void Start() {
        levelDistance = transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path.PathLength;
        Debug.Log(levelDistance);
        shipBoosting = false;
        shipModel = GameObject.Find("Ship").transform;
        shipCamera = GameObject.Find("Camera").GetComponent<Camera>();
        laserL = GameObject.Find("Laser_L").transform;
        laserR = GameObject.Find("Laser_R").transform;
        crosshair = GameObject.Find("Crosshair").transform;
        pauseMenu.SetActive(false);
    }

    void FixedUpdate() 
    {
        Vector3 scorePosition = scoreWrapper.transform.position;
        boostTextBox.GetComponent<UnityEngine.UI.Text>().text = boostFuel.ToString();

        switch (GameManager.currentState) {
            case GameManager.GameState.playing:
                pauseMenu.SetActive(false);
                ShipMovement();
                CrosshairMovement();
                PointShipAtCrosshair();
                SetCameraPosition();

                if (shipBoosting) {
                    boostFuel -= boostBurnRate * Time.fixedDeltaTime;

                    if (boostFuel < 0)
                        boostFuel = 0;
                    
                    transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = 30;
                } else {
                    transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = 20;
                }

                if (transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position == levelDistance) {
                    GameManager.currentState = GameManager.GameState.levelend;
                }
                break;
            
            case GameManager.GameState.levelend:
                Vector4 menuColour = levelMenu.transform.GetChild(0).GetComponent<RawImage>().color;
                Vector4 levelLabelColour = levelMenu.transform.GetChild(1).GetComponent<Text>().color;
                
                levelMenuOpacity = Mathf.Lerp(levelMenuOpacity, 180, 0.005f * Time.fixedDeltaTime);
                
                levelMenu.transform.GetChild(0).GetComponent<RawImage>().color = new Vector4(
                    menuColour.x, menuColour.y, menuColour.z, levelMenuOpacity);

                levelMenu.transform.GetChild(1).GetComponent<Text>().color = new Vector4(
                    levelLabelColour.x, levelLabelColour.y, levelLabelColour.z, levelMenuOpacity);

                scorePosition = new Vector3(
                    scorePosition.x, 
                    Mathf.Lerp(scorePosition.y, 400, 0.75f * Time.fixedDeltaTime), 
                    scorePosition.z);

                scoreWrapper.transform.position = scorePosition;
                levelMenu.SetActive(true);
                break;
        }
        
    }

    void ShipMovement() {
        shipModel.localPosition += new Vector3(movementDirection.x, movementDirection.y, 0) * shipSpeed * Time.fixedDeltaTime;

        // Don't allow the player to move beyond the limits of the camera
        Vector3 pos = shipModel.transform.localPosition;
        pos.x = Mathf.Clamp(pos.x, -15.0f, 15.0f);
        pos.y = Mathf.Clamp(pos.y, -10.5f, 10f);
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
        switch(GameManager.currentState) {
            case GameManager.GameState.playing:
            movementDirection = value.Get<Vector2>();
            break;
        }
    }

    public void OnAim(InputValue value)
    {
        switch(GameManager.currentState) {
            case GameManager.GameState.playing:
            aimingDirection = value.Get<Vector2>();
            break;
        }
    }
    
    public void OnFire(InputValue value)
    {
        switch(GameManager.currentState) {
            case GameManager.GameState.playing:
                Instantiate(laserPrefab, laserL.position, shipModel.transform.rotation);
                Instantiate(laserPrefab, laserR.position, shipModel.transform.rotation);
                laserSfx.Play();
                break; 
        }
    }

    public void OnBoost(InputValue value)
    {
        switch(GameManager.currentState) {
            case GameManager.GameState.playing:
            if (!shipBoosting && boostFuel > 0)
                shipBoosting = true;
            break;
        }
    }

    public void OnPause(InputValue value)
    {
        // Debug.Log("Pause button");
        switch(GameManager.currentState) {
            case GameManager.GameState.playing:
                pauseMenu.SetActive(true);
                GameManager.currentState = GameManager.GameState.paused;
                Time.timeScale = 0;
                break;

            case GameManager.GameState.paused:
                pauseMenu.SetActive(false);
                GameManager.currentState = GameManager.GameState.playing;
                Time.timeScale = 1;
                break;
        }
    }
}
