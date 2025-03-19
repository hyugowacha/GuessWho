public interface IPlayerStates
{
    public void EnterState(StateManager player);

    public void UpdatePerState();

    public void ExitState();
}