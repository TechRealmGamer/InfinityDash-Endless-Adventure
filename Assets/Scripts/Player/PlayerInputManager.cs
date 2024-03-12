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

        SwipeDetection.instance.swipePerformed += Direction => TouchInput(Direction);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void TouchInput(Vector2 direction)
    {
        if (direction.x > 0)
        {
            switchLane = 1;
        }
        else if (direction.x < 0)
        {
            switchLane = -1;
        }
        else if (direction.y > 0)
        {
            jump = true;
        }
        else if (direction.y < 0)
        {
            slide = true;
        }
    }
}
