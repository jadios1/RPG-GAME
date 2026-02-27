namespace RPG_Game;

public class Wall : Field
{
    public Wall(){}
    public override bool IsPassable()
    {
        return false;
    }

    public override char GetSymbol()
    {
        return '█';
    }
}