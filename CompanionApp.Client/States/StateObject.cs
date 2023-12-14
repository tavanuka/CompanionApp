namespace CompanionApp.Client.States;

public class StateObject
{
    public event Action? OnStateChange;
    protected virtual void NotifyStateChanged() => OnStateChange?.Invoke();
}