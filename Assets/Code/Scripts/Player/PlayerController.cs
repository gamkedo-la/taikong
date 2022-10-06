using UnityEngine;
using UnityEngine.UI;

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

    //empty gameobjects to hold where we want the laser to fire from 
    [SerializeField] GameObject leftLaser; 
    [SerializeField] GameObject rightLaser;
    private GameObject shootFrom;
    
    public GameObject laserPrefab;
    public float movementSpeed;
    public bool isBoosting = false;
    public Vector3 crosshairWorldPos;
    public float crosshairSpeed;

    void Start() {
        shipModel = GameObject.Find("Ship");
        playerCamera = GameObject.Find("Camera");
        crosshair = GameObject.Find("Crosshair");
        shootFrom = leftLaser;
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
        pos.x = Mathf.Clamp(pos.x, -11.0f, 11.0f);
        pos.y = Mathf.Clamp(pos.y, -8.5f, 8.0f);
        shipModel.transform.localPosition = pos;
    }

    void CrosshairMove(float x, float y, float speed) {
        crosshair.transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    void CrosshairColor() {
        Color crosshair_red = new Color(0.7f, 0.1f, 0.1f, 0.8f);
        Color crosshair_white = new Color(1f, 1f, 1f, 0.8f);
        RaycastHit hit;
        
        if (Physics.Raycast(shipModel.transform.position, shipModel.transform.TransformDirection(Vector3.forward), out hit)) {
            
            if(hit.transform.gameObject.CompareTag("Enemy")) {
                crosshair.GetComponent<Graphic>().color = crosshair_red;
            } else {
                crosshair.GetComponent<Graphic>().color = crosshair_white;
            }   
            
            Debug.DrawRay(shipModel.transform.position, hit.transform.position, Color.green);
            
        } 
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
        crosshairWorldPos = playerCamera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(crosshair.transform.position.x, crosshair.transform.position.y, 500f));
        shipModel.transform.LookAt(crosshairWorldPos);

        // CrosshairColor();
    }

    void FireLasers() {
        // Laser first attempt 3 - Raycast from player ship's position

        //alternate between sides of shooting
        if (shootFrom == leftLaser)
            shootFrom = rightLaser;
        else
            shootFrom = leftLaser;
        RaycastHit hit;
        if (Physics.Raycast(shipModel.transform.position, shipModel.transform.TransformDirection(Vector3.forward), out hit)) {
            GameObject laserShot = Instantiate(
                laserPrefab, shootFrom.transform.position, 
                Quaternion.LookRotation(hit.point - shipModel.transform.position));
        }

        
    }
}
