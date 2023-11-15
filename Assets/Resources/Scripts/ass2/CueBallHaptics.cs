using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CueBallHaptics : MonoBehaviour
{
    private XRBaseController controller;
    private Collider cueCollider;

    private void Update()
    {
        if (cueCollider == null)
        {
            GameObject cueHandle = GameObject.FindGameObjectWithTag("Cue");
            if (cueHandle != null)
            {
                cueCollider = cueHandle.GetComponent<Collider>();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cueCollider != null && collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Cue Handle collider collided with Ball collider");
        }
    }
}