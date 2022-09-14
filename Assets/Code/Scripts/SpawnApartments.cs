using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnApartments : MonoBehaviour
{

    public GameObject apartmentModule;
    public int unitsWide;
    public int unitsHigh;

    private int buildingWidth = 8;
    private int buildingHeight = 6;

    // Start is called before the first frame update
    void Start() {
        float curX = apartmentModule.transform.position.x;
        float curY = apartmentModule.transform.position.y;
        float curZ = apartmentModule.transform.position.z;

        for (int w = 0; w < unitsWide; w++) {
            curY = apartmentModule.transform.position.y;
            
            for (int h = 0; h < unitsHigh; h++) {
                Vector3 buildingPosition = new Vector3(curX, curY, curZ);
                GameObject apartmentUnit = Instantiate(apartmentModule, buildingPosition, apartmentModule.transform.rotation);
                curY = curY + buildingHeight;
            }

            curZ = curZ + buildingWidth;
        }
    }
}
