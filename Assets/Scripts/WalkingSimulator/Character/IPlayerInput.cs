namespace WalkingSimulator.Character
{
    public interface IPlayerInput
    {
        float GetVerticalAxis();
        float GetHorizontalAxis();
        bool GetJumpKeyDown();
    }
}