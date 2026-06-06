using RPG_Game.Events;

namespace RPG_Game.Species;

public class SkeletonSpecies : ISpecies,IEventVisitor
{
    private List<Enemy> _members = new();

    public void AddMember(Enemy enemy) => _members.Add(enemy);
    public void RemoveMember(Enemy enemy) => _members.Remove(enemy);


    public void OnNotify(IEvent gameEvent) => gameEvent.Accept(this);

    public void Visit(EnemyDiedEvent e)
    {
        foreach (var member in _members)
        {
            member.Attack += 3;
            member.Armor += 2;
        }
    }

    public void Visit(SoundEvent e) { }

}

