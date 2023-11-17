using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f; // Adjust the speed as needed
    private float moveRange; // Adjust the range as needed
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 parentX;

    // Start is called before the first frame update
    void Start()
    {
        Transform trackParentParent = transform.parent.parent;
        parentX = trackParentParent.position;
        startPosition = transform.position - parentX;
        moveRange = Mathf.Abs(startPosition.y) * 2;
        endPosition = new Vector3(-startPosition.x, startPosition.y, startPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();
    }

    void MoveObstacle()
    {
        float t = Mathf.Repeat(Time.time * moveSpeed, 2.0f) / 2.0f; // Use Mathf.Repeat for continuous movement
        t = Mathf.SmoothStep(0f, 1f, t);

        transform.position = Vector3.Lerp(startPosition + parentX, endPosition + parentX, t);

        // Change direction when reaching the end position
        if (t >= 0.99999f)
        {
            SwapStartEndPositions();
        }
    }

    void SwapStartEndPositions()
    {
        Vector3 temp = startPosition;
        startPosition = endPosition;
        endPosition = temp;
    }
}
