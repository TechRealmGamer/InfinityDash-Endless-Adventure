using System;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerInput playerInput;

    public int switchLane;
    public bool jump;
    public bool slide;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Start()
    {
        playerInput.Player.Jump.performed += ctx => jump = true;
        playerInput.Player.Slide.performed += ctx => slide = true;

        playerInput.Player.SwitchLane.performed += ctx =>
        {
            switchLane = (int) ctx.ReadValue<float>();
        };
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
