using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] levelParts;

    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        var instantiatedPrefabs = new List<GameObject>();
        foreach (var levelPart in levelParts)
        {
            var levelPrefab =GameObject.Instantiate(levelPart);
            instantiatedPrefabs.Add(levelPrefab);
        }
        
        // Set start point as first portion
        
        // Tie and set together each prefab
        // TODO: eventually rotate these all to match!
        for (var i = 0; i < instantiatedPrefabs.Count; i++)
        {

            if (i == instantiatedPrefabs.Count - 1)
            {
                // TODO: setup end level stuff
            }
            else
            {
                var smoothPathStart = instantiatedPrefabs[i].GetComponent<CinemachineSmoothPath>();
                var endOfFirst = smoothPathStart.m_Waypoints.Last();
                var smoothPathEnd = instantiatedPrefabs[i+1].GetComponent<CinemachineSmoothPath>();
                var startOfSecond = smoothPathStart.m_Waypoints.First();

                
                // Calc to offset these
                // Find GO point, find difference between the point I have. is it a world point
                // I think waypoint positions are in local space, so add to world space of gameobject?
                var worldSpacePoint1 = endOfFirst.position + instantiatedPrefabs[i].transform.position;
                var worldSpacePoint2 = startOfSecond.position + instantiatedPrefabs[i+1].transform.position;
                // TODO: rotate
                instantiatedPrefabs[i + 1].transform.position += worldSpacePoint1 - worldSpacePoint2;
            }
        }
        // Make a new path including all pieces
        var waypoints = new List<CinemachineSmoothPath.Waypoint>();
        foreach (var instantiatedPrefab in instantiatedPrefabs)
        {
            var smoothPath = instantiatedPrefab.GetComponent<CinemachineSmoothPath>();
            foreach (var smoothPathMWaypoint in smoothPath.m_Waypoints)
            {
                waypoints.Add(smoothPathMWaypoint);
                // TODO: need to adjust offsets for each thing
            }
        }

        instantiatedPrefabs.First().GetComponent<CinemachineSmoothPath>().m_Waypoints = waypoints.ToArray();
        
        // Set player to first path
        playerController.gameObject.transform.position = instantiatedPrefabs.First().transform.position;
        playerController.gameObject.GetComponent<CinemachineDollyCart>().m_Path =
            instantiatedPrefabs.First().GetComponent<CinemachineSmoothPath>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}