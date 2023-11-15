using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private Rigidbody rb;
    public float bounceForce = 1.1f; // Adjust the force as needed

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 forceDirection = Vector3.Reflect(rb.velocity.normalized, normal);
            rb.AddForce(forceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
