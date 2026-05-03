using RPG_Game.Fields;
using RPG_Game.Items;

namespace RPG_Game.Decorators;

public class WisdomDecorator : ItemDecorator
{
    
    public override string GetName() => _inner.GetName() + " (Wisdom)";

    public WisdomDecorator(Item inner) : base(inner)
    {
        _inner = inner;
    }
    
    public override void OnPickup(Player player, Field field)
    {
        player.Wisdom += 5;
        base.OnPickup(player, field);
    }
    


    public override void OnDrop(Player player)
    {
        player.Wisdom -= 5;
    }
}