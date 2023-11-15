using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StickInteraction : MonoBehaviour
{
    public Rigidbody stickRigidbody;
    public float forceMultiplier = 10.0f;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the stick collides with a ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Calculate the direction from the stick to the ball
                Vector3 direction = collision.contacts[0].point - stickRigidbody.position;

                // Apply a force to the ball
                ballRigidbody.AddForce(direction.normalized * forceMultiplier, ForceMode.Impulse);
            }
        }
    }
}

