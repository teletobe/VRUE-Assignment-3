using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    private new Transform transform;  
    public float score = 0;
    public Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        score = transform.position.x;
        textUI.text = score.ToString();
    }
}
