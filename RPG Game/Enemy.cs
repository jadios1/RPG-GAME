using RPG_Game.Events;
using RPG_Game.Logger;
using RPG_Game.Species;

namespace RPG_Game;

public class Enemy : IDisplayable, IObservable, IObserver, IEventVisitor
{
    public string Name;
    public int Health;
    public int Attack;
    public int Armor;
    private List<IObserver> _observers = new();
    public ISpecies Species { get; }
    public int X { get; set; }
    public int Y { get; set; }
    public Map? CurrentMap { get; set; }



    public Enemy(string name,int health,int attack,int armor, ISpecies species)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Armor = armor;
        Species = species;
        Species.AddMember(this);
        Subscribe(species);

    }
    
    public char GetSymbol()
    {
        return '!';
    }

    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);
    public void Notify(IEvent gameEvent)
    {
        foreach (var obs in _observers)
            obs.OnNotify(gameEvent);
    }


    public void OnNotify(IEvent gameEvent) => gameEvent.Accept(this);

    public void Visit(EnemyDiedEvent e)
    {
        throw new NotImplementedException();
    }

    public void Visit(SoundEvent e)
    {
        if (CurrentMap == null) return;
        int dist = CurrentMap.GetDistance(X, Y, e.X, e.Y);
        if (dist <= e.Range)
        {
            GameLog.Instance.Log($"{Name} at ({X},{Y}) heard {e.Source} from ({e.X},{e.Y}), distance: {dist}");
        }
    }
}