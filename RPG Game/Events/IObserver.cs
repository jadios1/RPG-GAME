namespace RPG_Game.Events;

public interface IObserver
{
    void OnNotify(IEvent gameEvent);
}
