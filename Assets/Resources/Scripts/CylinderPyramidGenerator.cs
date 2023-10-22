using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderPyramidGenerator : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public int numRows = 5;
    public int numCylindersInBase = 5;
    private float cylinderSpacing = 0.1f;
    public float structureSize = 1;

    void Start()
    {
        // Create an empty GameObject to act as the parent of all cylinders
        GameObject cylinderParent = new GameObject("CylinderParent");

        Vector3 startPosition = transform.position;
        //Rigidbody previousRigidbody = null; // To keep track of the previous cylinder's Rigidbody if it should be one entire Thing...

        float cylinderHeight = cylinderPrefab.transform.localScale.y;
        float cylinderRadius = cylinderPrefab.transform.localScale.x * 0.5f;

        for (int row = 0; row < numRows; row++)
        {
            for (int i = 0; i < numCylindersInBase - row; i++)
            {
                float xOffset = i * (cylinderRadius * 2 + cylinderSpacing) + (row * (cylinderRadius * 2 + cylinderSpacing) / 2);
                float zOffset = 1.5f * row * (cylinderHeight + cylinderSpacing);

                Vector3 position = startPosition + new Vector3(-4.5f + xOffset, zOffset + 0.95f+0.5f, -3);
                GameObject newCylinder = Instantiate(cylinderPrefab, position, Quaternion.identity);

                // Make the new cylinder a child of the cylinderParent
                newCylinder.transform.parent = cylinderParent.transform;
            }
        }

        cylinderParent.transform.localScale = new Vector3(structureSize, structureSize, structureSize);
    }
}
