using RPG_Game.Items.Weapon;

namespace RPG_Game.Decorators;

public abstract class WeaponDecorator : ItemDecorator
{
    protected Weapon _innerWeapon;
    
    public override int GetSoundRange() => _inner.GetSoundRange();

    
    public WeaponDecorator(Weapon inner) : base(inner)
    {
        _innerWeapon = inner;
    }
}