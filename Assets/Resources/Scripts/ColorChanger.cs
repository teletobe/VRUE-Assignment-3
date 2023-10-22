using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetColorRed()
    {
        _renderer.material.color = Color.red;
    }
    public void SetColorBlue()
    {
        _renderer.material.color = Color.blue;
    }
}
