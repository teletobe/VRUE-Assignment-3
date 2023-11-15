using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class BallInPocket : MonoBehaviour
{
    private Rigidbody rb;  // Reference to the Rigidbody component
    private bool isInsidePocket = false;  // Flag to track if the ball is inside the pocket
    private float moveDistance = 0.15f; // 20cm move distance
    public GameObject BallHolesScoreGameObject;

    private Vector3 lastVelocity;  // Store the last known velocity for the ball

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component on Start
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isInsidePocket)
        {
            // Handle collisions with other objects (e.g., stick cue) here
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pocket"))
        {
            string pocketName = other.name; // Get the name of the pocket

            if (!isInsidePocket)
            {
                isInsidePocket = true;
                // Enable gravity on the Rigidbody
                rb.useGravity = true;

                // Store the last known velocity
                lastVelocity = rb.velocity;

                // Move the ball 20 centimeters in the direction based on the pocket's name
                Vector3 moveDirection = Vector3.zero;

                switch (pocketName)
                {
                    case "Pocket 1":
                        moveDirection = new Vector3(1, 1, -1);
                        break;
                    case "Pocket 2":
                        moveDirection = new Vector3(1, -1, -1);
                        break;
                    case "Pocket 3":
                        moveDirection = new Vector3(1, 1, 1);
                        break;
                    case "Pocket 4":
                        moveDirection = new Vector3(1, -1, 1);
                        break;
                    case "Pocket 5":
                        moveDirection = new Vector3(-1, 1, -1);
                        break;
                    case "Pocket 6":
                        moveDirection = new Vector3(-1, -1, -1);
                        break;
                    case "Pocket 7":
                        moveDirection = new Vector3(-1, 1, 1);
                        break;
                    case "Pocket 8":
                        moveDirection = new Vector3(-1, -1, 1);
                        break;
                }

                Vector3 newPosition = transform.position + moveDirection * moveDistance;
                transform.position = newPosition;
                
                // ugly but keeping track of score in position of gameobject lol
                float x = BallHolesScoreGameObject.transform.position.x;
                BallHolesScoreGameObject.transform.position = new Vector3(x + 1, 0, 0);
                
            }
        }
    }
}