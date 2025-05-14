using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        moveInput = moveInput.normalized;
    }
}