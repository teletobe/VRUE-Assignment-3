using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty TriggerAction;
    [SerializeField] private InputActionProperty GripAction;

    private InputData _inputData;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _inputData = GetComponent<InputData>();

    }

    private void Update()
    {
        float triggerValue = TriggerAction.action.ReadValue<float>();
        float gripValue = GripAction.action.ReadValue<float>();

        anim.SetFloat("Trigger", triggerValue);
        anim.SetFloat("Grip", gripValue);
    }
}
