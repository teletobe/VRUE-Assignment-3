using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]
public class SpawnThrowable : MonoBehaviour
{
    private InputData _inputData;
    public GameObject GrabbablePrefab; // Assign your grabbable object prefab in the Inspector
    private bool rightTriggerBool = false;
    private GameObject spawnedObject; // Reference to the spawned grabbable object
    private float objectSpawnScale = 0.1f; // Initial scale of the object
    private float objectScale = 0.1f; // Initial scale of the object
    private Rigidbody objectRigidbody; // Add this line to declare the objectRigidbody
    private GameObject throwables;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
        // Create an empty GameObject to act as the parent of all cylinders
        throwables = new GameObject("Throwables");
    }

    // Update is called once per frame
    private void Update()
    {
        // Spawn an object when the right trigger is pressed
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerValue))
        {
            _inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition);
            if (rightTriggerValue && !rightTriggerBool)
            {
                rightTriggerBool = true;
                SpawnThrowableObject(rightPosition);
                Debug.Log("Spawned throwable!");
            }
            else if (!rightTriggerValue && rightTriggerBool) // Button is released and was pressed in the previous frame
            {
                rightTriggerBool = false; // Set button state to released
            }
        }

        // allow for size changing by increasing or decreasing distance between controllers
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerValue))
        {
            if (leftTriggerValue)
            {
                _inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition);
                _inputData._leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition);

                float distance = Vector3.Distance(rightPosition, leftPosition);
                float minScale = 0.1f; // Minimum scale
                float maxScale = 2.0f; // Maximum scale
                                       // Calculate the scaling factor based on the distance
                float scaleFactor = Mathf.Clamp(distance, minScale, maxScale) / 2;
                Vector3 newScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                if (spawnedObject)
                {
                    spawnedObject.transform.localScale = newScale;
                }
            }
        }

        increaseSizeOnA();
        decreaseSizeOnB();

        // Update object mass based on its size
        UpdateObjectMass();
    }

    private void SpawnThrowableObject(Vector3 rightPosition)
    {
        Vector3 spawnPosition = rightPosition;
        spawnPosition[2] = spawnPosition[2] - 0.3f;
        spawnPosition[1] = spawnPosition[1] + 0.5f;

        if (spawnedObject)
        {
            spawnedObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        spawnedObject = Instantiate(GrabbablePrefab, spawnPosition, Quaternion.identity);
        // Make the new cylinder a child of the cylinderParent
        spawnedObject.transform.parent = throwables.transform;
        Debug.Log("Successfully spawned object!");
        spawnedObject.GetComponent<MeshRenderer>().material.color = Color.green;

        // Set the initial scale of the object
        spawnedObject.transform.localScale = new Vector3(objectSpawnScale, objectSpawnScale, objectSpawnScale);

        UpdateObjectMass();
    }

    private void changeSizeByDistance()
    {
        _inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition);
        _inputData._leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition);

        float distance = Vector3.Distance(rightPosition, leftPosition);
        float minScale = 0.1f; // Minimum scale
        float maxScale = 2.0f; // Maximum scale
        // Calculate the scaling factor based on the distance
        float scaleFactor = Mathf.Clamp(distance, minScale, maxScale) / 2;
        Vector3 newScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        spawnedObject.transform.localScale = newScale;
    }

    private void increaseSizeOnA()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonAValue) && buttonAValue)
        {
            if (spawnedObject != null)
            {
                objectScale += 0.01f;
                spawnedObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
            }
        }
    }

    private void decreaseSizeOnB()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool buttonBValue) && buttonBValue)
        {
            if (spawnedObject != null)
            {
                if (objectScale > 0.01f)
                {
                    objectScale -= 0.01f;
                    spawnedObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
                }
            }
        }
    }

    // Function to calculate the mass based on the object's scale
    private float CalculateObjectMass(float scale)
    {
        return scale * scale;
    }

    private void UpdateObjectMass()
    {
        if (spawnedObject != null)
        {
            objectRigidbody = spawnedObject.GetComponent<Rigidbody>(); // Get the object's Rigidbody component
            // Calculate the mass based on the current scale and update the Rigidbody's mass
            objectRigidbody.mass = CalculateObjectMass(spawnedObject.transform.localScale.x*5);
            objectRigidbody.drag = spawnedObject.transform.localScale.x * 1.5f;
        }
    }
}