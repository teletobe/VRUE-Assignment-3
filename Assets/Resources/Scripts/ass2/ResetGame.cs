using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]
public class ResetGame : MonoBehaviour
{
    private InputData _inputData;

    public GameObject balls;
    public GameObject walls;
    public GameObject pockets;

    public GameObject BallHolesGameObject;
    public GameObject HitsTrackerGameObject;

    private bool buttonABool = false;

    private List<PositionAndRotationAndScale> ballsStats = new List<PositionAndRotationAndScale>();
    private List<PositionAndRotationAndScale> wallsStats = new List<PositionAndRotationAndScale>();
    private List<PositionAndRotationAndScale> pocketsStats = new List<PositionAndRotationAndScale>();


    // to ensure white ball is randomly placed as well
    public GameObject objectToSpawn; 
    public Transform initialSphere; 
    public float spawnRadius = 0.29f;
    public float minDistance = 0.1f;
    public Text textUI;
    public Text hitsTextUI;
    public Text timerUI;
    public GameObject timerToReset;

    public Scrollbar scrollbar;



    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
        StoreInitialPositions();
    }

    // Function to store initial positions and rotations of objects
    void StoreInitialPositions()
    {
        StoreObjectTransforms(balls, ballsStats);
        StoreObjectTransforms(walls, wallsStats);
        StoreObjectTransforms(pockets, pocketsStats);
    }

    void StoreObjectTransforms(GameObject obj, List<PositionAndRotationAndScale> list)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            
            Transform child = obj.transform.GetChild(i);
            PositionAndRotationAndScale data = new PositionAndRotationAndScale(child.transform.position,
                child.transform.rotation, child.transform.localScale);
            list.Add(data);
        }
    }

    // Function to reset objects to their initial positions
    public void ResetObjectsToInitialPositions()
    {
        ResetObjectTransforms(balls, ballsStats);
        ResetObjectTransforms(walls, wallsStats);
        ResetObjectTransforms(pockets, pocketsStats);
        SpawnCueBallRandomly();
        ResetObjectTransforms(balls, ballsStats);
        SpawnCueBallRandomly();
        BallHolesGameObject.transform.position = new Vector3(0, 0, 0);
        HitsTrackerGameObject.transform.position = new Vector3(0, 0, 0);
        hitsTextUI.text = "0";
        textUI.text = "1";
        scrollbar.value = 0.4f;
        timerToReset.transform.position = new Vector3(-1, 0, 0);
        timerUI.text = "00:00";
    }

    void ResetObjectTransforms(GameObject obj, List<PositionAndRotationAndScale> list)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            PositionAndRotationAndScale data = list[i];
            child.position = data.position;
            child.rotation = data.rotation;
            child.localScale = data.scale;
            // Reset the child object's velocity to 0
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                // Disable gravity for objects with the "ball" tag
                if (child.CompareTag("Ball"))
                {
                    rb.useGravity = false;
                }
            }
        }
    }

    void SpawnCueBallRandomly()
    {
        if (objectToSpawn == null || initialSphere == null)
        {
            Debug.LogError("Please assign the object to spawn and the initial sphere in the Inspector.");
            return;
        }

        // Get the initial position of a sphere in the middle
        Vector3 initialPosition = initialSphere.position;

        // Calculate a random offset along a single axis (e.g., the x-axis)
        Random.InitState(System.Environment.TickCount); // Initialize random seed with the current time
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

    // Update is called once per frame
    void Update()
    {

        // Spawn an object when the right trigger is pressed
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonAValue))
        {

            if (buttonAValue && !buttonABool)
            {
                buttonABool = true;
                ResetObjectsToInitialPositions();
                SpawnCueBallRandomly();
                textUI.text = "1";
                scrollbar.value = 1.0f;
            }
            else if (!buttonAValue && buttonABool) // Button is released and was pressed in the previous frame
            {
                buttonABool = false; // Set button state to released
            }
        }
    }
}
