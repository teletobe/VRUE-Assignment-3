using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
[RequireComponent(typeof(InputData))]

public class TimeRewinder : MonoBehaviour
{
    private InputData _inputData;
    private bool isRewinding = false;
    //private bool leftButtonBool = false;
    private Rigidbody cylinderRb;

    private List<PositionAndRotation> positionsAndRotations;


    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
        positionsAndRotations = new List<PositionAndRotation>();
        cylinderRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            cylinderRb.isKinematic = true;
            Rewind();
        }
        else
        {
            cylinderRb.isKinematic = false;
            Record();
        }
    }

    void Record()
    {
        // dont record more than 10 seconds of positons to not get memory overload
        if (positionsAndRotations.Count > Math.Round(10f * (1f / Time.fixedDeltaTime)))
        {
            // remove at the bottom of list if full
            positionsAndRotations.RemoveAt(positionsAndRotations.Count-1);
        }
        positionsAndRotations.Insert(0, new PositionAndRotation(transform.position, transform.rotation));
    }

    private void Rewind()
    {
        if (positionsAndRotations.Count > 0)
        {
            PositionAndRotation positionsAndRotation = positionsAndRotations[0];
            transform.position = positionsAndRotation.position;
            transform.rotation = positionsAndRotation.rotation;
            positionsAndRotations.RemoveAt(0);
        }
        else
        {
            isRewinding = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        isRewinding =
                (_inputData._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftButtonXValue) &&
                 leftButtonXValue);

    }

}
