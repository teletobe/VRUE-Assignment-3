using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueHittingBallManager : MonoBehaviour
{
    private int hits;
    // Start is called before the first frame update
    public int getHits()
    {
        return hits;
    }

    public void isHit()
    {
        hits++;
    }
}
