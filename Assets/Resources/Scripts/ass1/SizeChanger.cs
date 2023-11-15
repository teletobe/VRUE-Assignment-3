using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI; // Don't forget this line

[RequireComponent(typeof(InputData))]
public class SizeChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public InputDevice _rightController;
    public InputDevice _leftController;

    private InputData _inputData;

    private float scaleFactorToShare = 1;

    public GameObject walls;
    public GameObject pockets;

    public Text textUI;
    public Scrollbar scrollbar;

    void Start()
    {
        _inputData = GetComponent<InputData>();
    }

public void ChangeSizeByDistance(float distance)
    {
        ChangeScale(walls, distance);
        ChangeScale(pockets, distance);
        //SpawnCueBallRandomly();
    }

    void ChangeScale(GameObject obj, float distance)
    {
        // You can set a minimum and maximum scaling range if needed
        float minScale = 0.4f; // Minimum scale
        float maxScale = 1.0f; // Maximum scale

        // Calculate the scaling factor based on the distance
        float scaleFactor = Mathf.Clamp(distance, minScale, maxScale)*2f;
        scaleFactorToShare = scaleFactor;

        // Apply the scale to the object
        Vector3 newScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        obj.transform.localScale = newScale;
    }


    // Update is called once per frame
    void Update()
    {
        _inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition);
        _inputData._leftController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition);

        float distance = Vector3.Distance(rightPosition, leftPosition);

        // Scale the object based on the distance
        //ScaleObjectBasedOnDistance(distance);

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftButtonXValue) &&
            leftButtonXValue)
        {
            ChangeSizeByDistance(distance);
            textUI.text = scaleFactorToShare.ToString();
            scrollbar.value = Mathf.InverseLerp(0.8f, 2.0f, scaleFactorToShare);

        }

        // Debug.Log(distance);
    }

    public void OnScrollbarValueChanged()
    {
        scaleFactorToShare = Mathf.Lerp(0.8f, 2.0f, scrollbar.value);
        textUI.text = scaleFactorToShare.ToString();
        Vector3 newScale = new Vector3(scaleFactorToShare, scaleFactorToShare, scaleFactorToShare);
         
        walls.transform.localScale = newScale;
        pockets.transform.localScale = newScale;
    }

    public float getScaleFactor()
    {
        return scaleFactorToShare;
    }

}