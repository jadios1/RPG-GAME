namespace RPG_Game.Events;

public class EnemyDiedEvent : IEvent
{
    public Enemy Who { get; }
    public EnemyDiedEvent(Enemy who) { Who = who; }
    
    public void Accept(IEventVisitor visitor) => visitor.Visit(this);
}