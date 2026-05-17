namespace RPG_Game.Events;

public interface IEvent
{
    void Accept(IEventVisitor visitor);

}
