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
    [SerializeField] int enemyCount;
    
    bool pursueActive = false;
    GameObject shipsContainer;
    Cinemachine.CinemachineDollyCart flightpath;
    
    void Start()
    {
        flightpath = GetComponent<Cinemachine.CinemachineDollyCart>();
        shipsContainer = transform.GetChild(0).gameObject;   
        shipsContainer.SetActive(false);
        shipsContainer.transform.localPosition = new Vector3(0, startingHeight, 0);

        for (int e = 0; e < enemyCount; e++ ) 
        {
            int random = Random.Range(-9, 9);
            GameObject spawnedEnemy = Instantiate(enemyType, shipsContainer.transform.position, shipsContainer.transform.rotation);
            spawnedEnemy.transform.parent = shipsContainer.transform;
            spawnedEnemy.transform.localPosition = new Vector3(random, 0, 0);
        }
    }

    void FixedUpdate() {
        if (pursueActive && shipsContainer.transform.localPosition.y > 0) {
            float currentHeight = shipsContainer.transform.localPosition.y;
            float newHeight = Mathf.Lerp(currentHeight, 0, 0.5f * Time.deltaTime);
            shipsContainer.transform.localPosition = new Vector3(0, newHeight, 0);
        } 
    }
    
    private void OnTriggerEnter(Collider other)
    {           
        if(other.CompareTag("PlayerDetect"))
        {
            pursueActive = true;
            shipsContainer.SetActive(true);
            flightpath.m_Path = other.transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path;
            flightpath.m_Speed = other.transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed;
            flightpath.m_Position = other.transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position + 10;
        }
    }
}
