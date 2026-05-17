using RPG_Game.Events;

namespace RPG_Game.Species;

public interface ISpecies : IObserver
{
    void AddMember(Enemy enemy);
    void RemoveMember(Enemy enemy);

}
