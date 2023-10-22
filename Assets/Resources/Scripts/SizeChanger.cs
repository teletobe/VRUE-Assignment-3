using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]
public class SizeChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public InputDevice _rightController;
    public InputDevice _leftController;
    private InputData _inputData;
    public GameObject grabbablePrefab; // Assign your grabbable object prefab in the Inspector
    private bool isObjectGrabbed = false;
    private Transform grabbedByHand;


    void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    // Update is called once per frame
    void Update()
    {
        _inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition);
        _inputData._leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition);

        float distance = Vector3.Distance(rightPosition, leftPosition);

        // Scale the object based on the distance
        //ScaleObjectBasedOnDistance(distance);

        if (isObjectGrabbed)
        {
            Debug.Log("about to call scaling function...");
            ScaleObjectBasedOnDistance(distance);
        }

        // Debug.Log(distance);
    }


    public void GrabObject(GameObject grabbedObject)
    {
        isObjectGrabbed = true;
        Debug.Log("should be the grabbed Object????");
        Debug.Log(grabbedObject);
        //grabbedByHand = grabbedObject.transform;
        grabbedObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        
    }
//     public void GrabObject()
//     {
//         isObjectGrabbed = true;
//         Debug.Log("should be true?");
//         Debug.Log(isObjectGrabbed);
//     }


    // Implement a method to release the object
    public void ReleaseObject()
    {
        isObjectGrabbed = false;
        Debug.Log("should be false?");
        Debug.Log(isObjectGrabbed);
        //grabbedByHand = null;
    }

    void ScaleObjectBasedOnDistance(float distance)
    {
        Debug.Log("Scaling now for object: " + grabbablePrefab.name);

        // You can set a minimum and maximum scaling range if needed
        float minScale = 0.1f; // Minimum scale
        float maxScale = 2.0f; // Maximum scale

        // Calculate the scaling factor based on the distance
        float scaleFactor = Mathf.Clamp(distance, minScale, maxScale)/2;
        Debug.Log("Scale Factor");
        Debug.Log(scaleFactor);


        // Apply the scale to the object
        Vector3 newScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        grabbablePrefab.transform.localScale = newScale;
        Debug.Log(grabbablePrefab.transform.localScale = newScale);
        Debug.Log(grabbablePrefab.transform.localScale);

    }
}