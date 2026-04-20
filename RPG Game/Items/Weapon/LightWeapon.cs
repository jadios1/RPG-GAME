namespace RPG_Game.Items.Weapon;

public abstract class LightWeapon : Weapon
{
    public override int AcceptAttack(IAttackVisitor visitor, Player player)
    {
        Console.SetCursorPosition(0, 23);
        Console.WriteLine("LightWeapon.AcceptAttack called".PadRight(50));

        return visitor.VisitLight(this,player);
    }

    public override int AcceptDefense(IDefenseVisitor visitor, Player player)
    {
        return visitor.VisitLightDefense(this,player);

    }
}