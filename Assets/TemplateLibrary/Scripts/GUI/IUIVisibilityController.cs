using System;

public interface IUIVisibilityController
{
    void Show();
    void Hide();
    void ShowImmediately();
    void HideImmediately();
}

public class SubscriptionEvent
{
    private Action sEvent;

    public void Subscribe(Action listener)
    {
        this.sEvent += listener;
    }

    public void Unsubscibe(Action listener)
    {
        this.sEvent -= listener;
    }
    public void Invoke()
    {
        sEvent?.Invoke();
    }
}
