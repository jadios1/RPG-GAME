namespace RPG_Game.Events;

public interface IObservable
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void Notify(IEvent gameEvent);
}
