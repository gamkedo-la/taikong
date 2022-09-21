using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildings : MonoBehaviour
{

    public GameObject apartmentModule;
    public int unitsWide;
    public int unitsHigh;

    public int buildingWidth;
    public int buildingHeight;

    // Start is called before the first frame update
    void Start() {
        for (int w = 0; w < unitsWide; w++) {
            for (int h = 0; h < unitsHigh; h++) {
                GameObject apartmentUnit = Instantiate(
                    apartmentModule, 
                    apartmentModule.transform.position,
                    apartmentModule.transform.rotation,
                    transform
                );
                apartmentUnit.transform.localPosition = new Vector3(0, h * buildingHeight, w * buildingWidth);
            }
        }
    }
}
