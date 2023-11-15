using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueCollidingBall : MonoBehaviour
{
    public CueHittingBallManager manager;
    public GameObject HitsGameObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            manager.isHit();
            // ugly but keeping track of score in position of gameobject lol
            float x = HitsGameObject.transform.position.x;
            HitsGameObject.transform.position = new Vector3(x + 1, 0, 0);
        }
    }
}