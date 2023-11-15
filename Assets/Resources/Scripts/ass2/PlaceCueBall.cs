using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlaceCueBall : MonoBehaviour
{
    public GameObject objectToSpawn; // The object you want to spawn
    public Transform initialSphere;  // The initial sphere in the scene
    public float spawnRadius = 0.29f;
    public float minDistance = 0.1f;

    void Start()
    {
        SpawnObjectRandomly();
    }

    void SpawnObjectRandomly()
    {
        if (objectToSpawn == null || initialSphere == null)
        {
            Debug.LogError("Please assign the object to spawn and the initial sphere in the Inspector.");
            return;
        }

        // Get the initial position of the sphere
        Vector3 initialPosition = initialSphere.position;

        // Calculate a random offset along a single axis (e.g., the x-axis)
        float randomOffsetX = Random.Range(-spawnRadius, spawnRadius);
        float randomOffsetY = Random.Range(-spawnRadius, spawnRadius);
        float randomOffsetZ = Random.Range(-spawnRadius, spawnRadius);

        randomOffsetX += (randomOffsetX <= 0) ? -minDistance : minDistance;
        randomOffsetY += (randomOffsetY <= 0) ? -minDistance : minDistance;
        randomOffsetZ += (randomOffsetZ <= 0) ? -minDistance : minDistance;

        // Set the position of the object
        objectToSpawn.transform.position = new Vector3(initialPosition.x + randomOffsetX, initialPosition.y + randomOffsetY,
            initialPosition.z + randomOffsetZ);
    }
}