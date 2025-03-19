public interface IPlayerStates
{
    public void EnterState(Player player);

    public void UpdatePerState();

    public void ExitState();
}