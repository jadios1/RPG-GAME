using System.Reflection;

namespace RPG_Game;

public class Enemy : IDisplayable
{
    public string Name;
    public int Health;
    public int Attack;
    public int Armor;

    public Enemy(string name,int health,int attack,int armor)
    {
        Name = name;
        Health = health;
        attack = attack;
        armor = armor;
    }
    
    public char GetSymbol()
    {
        return '!';
    }
}