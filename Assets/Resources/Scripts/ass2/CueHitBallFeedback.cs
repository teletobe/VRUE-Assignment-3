using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CueHitBallFeedback : MonoBehaviour
{

    public CueHittingBallManager manager;
    private int lastHitAmount = 0;

    public float intensity = 0.8f; // Adjust the haptic feedback intensity
    public float pulseDuration = 0.025f; // Adjust the haptic feedback duration
    public float pulseInterval = 0.1f; // Adjust the interval between pulses
    private bool isInRange;


    private Collider cueHandleCollider;
    private XRBaseController controller;
    public GameObject HitsGameObject;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<XRBaseController>();

        if (controller == null)
        {
            Debug.LogError("XRBaseController component not found on this GameObject.");
        }

        StartCoroutine(PlayHapticFeedback());

        cueHandleCollider = GameObject.FindGameObjectWithTag("Cue Handle").GetComponent<Collider>();

        // Ensure the cue handle collider is set to be a trigger in the Unity Inspector.
        if (cueHandleCollider != null)
        {
            cueHandleCollider.isTrigger = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other == cueHandleCollider)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == cueHandleCollider)
        {
            isInRange = false;
        }
    }

    private IEnumerator PlayHapticFeedback()
    {
        while (true)
        {
            yield return new WaitForSeconds(pulseInterval);
            if (manager.getHits() > lastHitAmount && isInRange)
            {
                lastHitAmount = manager.getHits();
                controller.SendHapticImpulse(intensity, pulseDuration);
            }
        }

    }
}
