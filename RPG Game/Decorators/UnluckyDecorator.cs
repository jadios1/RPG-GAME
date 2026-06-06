using RPG_Game.Fields;
using RPG_Game.Items;

namespace RPG_Game.Decorators;

public class UnluckyDecorator : ItemDecorator
{
    public UnluckyDecorator(Item inner) : base(inner)
    {
        _inner = inner;
    }
    
    public override string GetName() => _inner.GetName() + " (Unlucky)";

    public override void OnPickup(Player player, Field field)
    {
        player.Luck -= 5;
        base.OnPickup(player, field);
    }
    


    public override void OnDrop(Player player)
    {
        player.Luck += 5;
    }

    
}