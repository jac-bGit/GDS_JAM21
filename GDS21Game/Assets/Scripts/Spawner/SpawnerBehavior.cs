/*
Component for handling multiple objects spawn. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour
{
    //[SerializeField] private GameObject m_Player;
    //[SerializeField] private Spawner[] m_aSpawners;

    [SerializeField] private GameObject[] m_aSpawnObjects;
    [SerializeField] private BoxCollider m_SpawnArea;

    // Components 


    //------------------------------------------------------
    // Mono functions 
    //------------------------------------------------------

    //------------------------------------------------------
    void Start()
    {
        // Disable trigger meshes 
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer render in renderers)
        {
            render.enabled = false;
        }
    }

    //------------------------------------------------------
    void Update()
    {
        
    }

    //------------------------------------------------------
    // Private functions 
    //------------------------------------------------------

    //------------------------------------------------------
    // Spawn object randomly in area
    public void SpawnObject()
    {
        // Pick object 
        GameObject go = ObjectToSpawn();

        // Pick area 
        Vector3 spawnPos = SpawnAreaPosition();
        spawnPos += m_SpawnArea.transform.position;

        // Spawn 
        GameObject spawned = Instantiate(go, spawnPos, new Quaternion(), null);
    }

    //------------------------------------------------------
    // Pick random object from spawn array 
    private GameObject ObjectToSpawn()
    {
        int len = m_aSpawnObjects.Length;

        // Signle
        if (len == 1)
            return m_aSpawnObjects[0];
        else if (len > 1)
        {
            // Pick random from array 
            int rnd = Random.Range(0, len - 1);
            return m_aSpawnObjects[rnd];
        }

        // Empty spawn objects array 
        return null;
    }

    //------------------------------------------------------
    // Pick random position in spawner area 
    private Vector3 SpawnAreaPosition()
    {
        // sizes 
        float x = m_SpawnArea.size.x / 2;
        float z = m_SpawnArea.size.z / 2;

        // Pick positions 
        x = Random.Range(-x, x);
        z = Random.Range(-z, z);

        // Set position 
        return new Vector3(x, 0, z);
    }
}

//------------------------------------------------------
// Spawner configrable class 
[System.Serializable]
public class Spawner 
{
    // Attributes
    [SerializeField] public GameObject m_SpawnObject;
    [SerializeField] public Vector2 m_vSpawnDistRange;
    [SerializeField] public float m_fDistanceFromPlayer;

    // Distance to spawn 
    [HideInInspector] public float m_fSpawnDist;

    //------------------------------------------------------
    // Setup spawn distance from spawn ranges 
    public void SetupSpawnDistance(Vector3 position)
    {
        m_fSpawnDist = position.x;
        m_fSpawnDist += Random.Range(m_vSpawnDistRange.x, m_vSpawnDistRange.y);
    }
}
