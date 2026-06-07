using RPG_Game.Logger;
using RPG_Game.Visitors;
using System;
using System.Collections.Generic;

namespace RPG_Game;

public class Controller
{
    private Dictionary<ConsoleKey, (IAttackVisitor, IDefenseVisitor)> _combatActions;

    public Controller()
    {
        _combatActions = new Dictionary<ConsoleKey, (IAttackVisitor, IDefenseVisitor)>
        {
            { ConsoleKey.D1, (new NormalAttackVisitor(), new NormalDefenseVisitor()) },
            { ConsoleKey.D2, (new StealthAttackVisitor(), new StealthDefenseVisitor()) },
            { ConsoleKey.D3, (new MagicalAttackVisitor(), new MagicalDefenseVisitor()) },
        };
    }

    public void HandleInput(Model model,ConsoleKey pressedKey)
    {

        if (model.ShowFullLog)
        {
            model.ShowFullLog = false;
            return;
        }

        if (model.IsInCombat)
        {
            HandleCombatInput(pressedKey, model);
            return;
        }

        bool actionSuccess = true;

        switch (pressedKey)
        {
            case ConsoleKey.W: actionSuccess = model.MovePlayer(0, -1); break;
            case ConsoleKey.A: actionSuccess = model.MovePlayer(-1, 0); break;
            case ConsoleKey.S: actionSuccess = model.MovePlayer(0, 1); break;
            case ConsoleKey.D: actionSuccess = model.MovePlayer(1, 0); break;
            case ConsoleKey.E: actionSuccess = model.PickUpItem(); break;
            case ConsoleKey.R: actionSuccess = model.DropItem(); break;
            case ConsoleKey.Z: actionSuccess = model.EquipLeft(); break;
            case ConsoleKey.X: actionSuccess = model.EquipRight(); break;
            case ConsoleKey.D1: actionSuccess = model.ChangeSlot(0); break;
            case ConsoleKey.D2: actionSuccess = model.ChangeSlot(1); break;
            case ConsoleKey.D3: actionSuccess = model.ChangeSlot(2); break;
            case ConsoleKey.F: actionSuccess = model.StartCombat(); break;
            case ConsoleKey.J: model.ShowFullLog = true; break;
            default:
                GameLog.Instance.Log("Pressed unknown key!");
                actionSuccess = false;
                break;
        }
    }

    private void HandleCombatInput(ConsoleKey key, Model model)
    {
        if (key == ConsoleKey.F)
        {
            model.FleeCombat();
            return; 
        }

        if (_combatActions.TryGetValue(key, out var visitors))
        {
            model.ResolveCombatRound(visitors.Item1, visitors.Item2);
        }
        else
        {
            GameLog.Instance.Log("Pressed unknown key during combat!");
        }
    }
}