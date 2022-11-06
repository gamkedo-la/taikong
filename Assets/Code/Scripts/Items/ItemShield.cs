using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : MonoBehaviour
{
    private Transform pickupIcon;
    private float rotationSpeed = 50f;

    void Start() {
        pickupIcon = transform.GetChild(0).transform;
    }
    
    void Update() {
        float currentRotation = rotationSpeed * Time.deltaTime;
        pickupIcon.Rotate(currentRotation, -currentRotation, currentRotation);
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Through the loop!");
    }

    void RemoveItem() {
        // TODO animate and remove item ring from the scene
        Destroy(gameObject);
    }
}
