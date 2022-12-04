using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursue : MonoBehaviour
{
    [Header("Pursuit settings")]
    [SerializeField] float timeToLive;
    [SerializeField] float startingHeight;
    [SerializeField] float moveToPositionSpeed;
    
    [Header("Enemy Details")]
    [SerializeField] GameObject enemyType;
    [SerializeField] Vector3[] startPositions;
    
    bool pursueActive = false;
    GameObject shipsContainer;
    Cinemachine.CinemachineDollyCart flightpath;
    
    void Start()
    {
        flightpath = GetComponent<Cinemachine.CinemachineDollyCart>();
        shipsContainer = transform.GetChild(0).gameObject;   
        shipsContainer.SetActive(false);
        shipsContainer.transform.localPosition = new Vector3(0, startingHeight, 0);

        for (int e = 0; e < startPositions.Length; e++ ) 
        {
            GameObject spawnedEnemy = Instantiate(enemyType, shipsContainer.transform.position, shipsContainer.transform.rotation);
            spawnedEnemy.transform.parent = shipsContainer.transform;
            spawnedEnemy.transform.localPosition = startPositions[e];
        }
    }

    void FixedUpdate() 
    {
        float currentHeight = shipsContainer.transform.localPosition.y;

        if (pursueActive && shipsContainer.transform.localPosition.y > 0) 
        {
            float newHeight = Mathf.Lerp(currentHeight, 0, 0.5f * Time.fixedDeltaTime);
            shipsContainer.transform.localPosition = new Vector3(0, newHeight, 0);
            
            if (timeToLive != -1) {
                timeToLive -= Time.fixedDeltaTime * 5;
            }
        } 
        else
        {
            float newHeight = Mathf.Lerp(currentHeight, startingHeight, 0.75f * Time.fixedDeltaTime);
            shipsContainer.transform.localPosition = new Vector3(0, newHeight, 0);
        }

        if (timeToLive <= 0 && timeToLive != -1)
        {
            pursueActive = false;
            Destroy(gameObject, 5f);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {           
        if(other.CompareTag("PlayerDetect"))
        {
            Debug.Log("Player spotted");
            pursueActive = true;
            shipsContainer.SetActive(true);
            flightpath.m_Path = other.transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path;
            flightpath.m_Speed = other.transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed + 1;
            flightpath.m_Position = other.transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position + 20;
        }
    }
}
