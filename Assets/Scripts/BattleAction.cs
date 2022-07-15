using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleAction {

    public abstract string GetActionIcon();

    public abstract string GetActionText();

    public abstract void DoAction();
}

[System.Serializable]
public class AttackAction : BattleAction {
    Enemy enemy;
    DiceType diceType;
    int amount;

    public void DoAction() {
        throw new System.NotImplementedException();
    }

    public string GetActionIcon() {
        return "attackIcon.png";
    }

    public string GetActionText() {
        string attackString = amount + DiceType.D4.ToString();

        if(enemy.Strength > 0) {
            attackString += " + " + enemy.Strength;
        } else if(enemy.Strength < 0) {
            attackString += " - " + enemy.Strength;
        }

        return attackString;
    }
}

public class DefendAction : BattleAction {

    Enemy enemy;
    DiceType diceType;
    int amount;

    public void DoAction() {
        throw new System.NotImplementedException();
    }

    public string GetActionIcon() {
        return "blockIcon.png";
    }

    public string GetActionText() {
        throw new System.NotImplementedException();
    }
}
