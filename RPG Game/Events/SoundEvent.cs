namespace RPG_Game.Events;

public class SoundEvent : IEvent
{
    public int X { get; }
    public int Y { get; }
    public int Range { get; }
    public string Source { get; }
    
    public SoundEvent(int x, int y, int range, string source)
    {
        X = x; 
        Y = y; 
        Range = range; 
        Source = source;
    }
    
    public void Accept(IEventVisitor visitor) => visitor.Visit(this);

}
