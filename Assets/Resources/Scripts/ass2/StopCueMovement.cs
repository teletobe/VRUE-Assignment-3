using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StopCueMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    public void StopCueStick()
    {
        // Set the Rigidbody's velocity to zero to stop all motion.
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }
}
