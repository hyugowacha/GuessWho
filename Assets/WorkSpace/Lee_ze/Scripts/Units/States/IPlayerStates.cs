public interface IPlayerStates
{
    public void EnterState(PlayerControl player);

    public void UpdatePerState();

    public void ExitState();
}