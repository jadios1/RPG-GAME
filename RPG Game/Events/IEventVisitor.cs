namespace RPG_Game.Events;

public interface IEventVisitor
{
    void Visit(EnemyDiedEvent e);
    void Visit(SoundEvent e);

}