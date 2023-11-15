using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedbackOnHover : MonoBehaviour
{
    private XRBaseController controller; // Define the interactor variable

    [Range(0, 1)]
    public float intensity = 0.5f;
    public float pulseDuration = 0.1f;

    private void Start()
    {
        controller = GetComponent<XRBaseController>();
        if (controller == null)
        {
            Debug.LogError("XRBaseController component not found on this GameObject.");
        }
    }

    public void StartHapticPulse()
    {
        controller.SendHapticImpulse(intensity, pulseDuration);
    }
}