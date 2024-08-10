using UnityEngine;

public class PlayerInput : IPlayerInput
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public float GetVerticalAxis() => Input.GetAxisRaw(Vertical);
    public float GetHorizontalAxis() => Input.GetAxisRaw(Horizontal);
    public bool GetJumpKeyDown() => Input.GetKeyDown(KeyCode.Space);
}