using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemySpawner : MonoBehaviour
{
    public GameObject prefab;
    public int numberOfPrefabs;
    public float boundingRadius;
    private GameHUD _gameHUD;
    void Start()
    {
        _gameHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<GameHUD>();
        #region Spawning
        int spawns = 0;
        do
        {
            if (RandomPoint(transform.position, boundingRadius, out Vector3 position))
            {
                Instantiate(prefab, position, transform.rotation, transform);
                spawns++;
            }
        } while (spawns < numberOfPrefabs);
        #endregion
    }


    void Update()
    {
        
    }
    public bool RandomPoint(Vector3 center, float radius, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
