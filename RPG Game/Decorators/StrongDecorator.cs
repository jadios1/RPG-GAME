using RPG_Game.Items.Weapon;

namespace RPG_Game.Decorators;

public class StrongDecorator : WeaponDecorator
{
    public StrongDecorator(Weapon inner) : base(inner)
    {
        _innerWeapon = inner;
        _innerWeapon.Damage += 5;
    }

    public override string GetName() => _innerWeapon.GetName() + " (Strong)";
}