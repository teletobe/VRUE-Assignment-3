using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedbackManager : MonoBehaviour
{
    private int grabbingHands;
    private bool isGrabbedByLeft = false;
    private bool isGrabbedByRight = false;

    private void Update()
    {
    }

    public void setIsGrabbedLeft()
    {
        isGrabbedByLeft = true;
    }

    public void unsetIsGrabbedLeft()
    {
        isGrabbedByLeft = false;
    }

    public void setIsGrabbedRight()
    {
        isGrabbedByRight = true;
    }

    public void unsetIsGrabbedRight()
    {
        isGrabbedByRight = false;
    }
    public bool getIsGrabbedByLeft()
    {
        return isGrabbedByLeft;
    }

    public bool getIsGrabbedByRight()
    {
        return isGrabbedByRight;
    }

    public bool isGrabbedByAny()
    {
        // Returns true if no hands are grabbing the stick
        return isGrabbedByLeft || isGrabbedByRight;
    }
}
