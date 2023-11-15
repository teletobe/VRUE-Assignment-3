using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitsTracker : MonoBehaviour
{
    private new Transform transform;  
    public float hits = 0;
    private int actualHits = 0;
    public Text textUI;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (hits + 1 < transform.position.x){
            hits = transform.position.x;
            actualHits += 1;
            textUI.text = actualHits.ToString();
        }

        if (transform.position.x == 0)
        {
            hits = 0;
        }

        if (hits == 0)
        {
            actualHits = 0;
        }
    }
}
