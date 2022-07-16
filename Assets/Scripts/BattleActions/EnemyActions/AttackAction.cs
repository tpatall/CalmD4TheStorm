using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour, BattleAction {

    public Enemy enemy;
    public DiceType diceType;
    public int diceAmount;

    public void DoAction() {
        Debug.Log(enemy.name + " is dealing " + diceAmount + "(" + diceType + " + " + enemy.Strength + ") damage!");

        for(int i = 0; i < diceAmount; i++) {
            int damage = DiceHelper.GetRandomFromDice(diceType) + enemy.Strength;

            Player.Instance.TakeDamage(damage);

            Debug.Log("Hit for " + damage + " damage!");
        }
    }

    public string GetActionIcon() {
        return "attackIcon";
    }

    public string GetActionText() {
        string attackString = "";
        
        if(diceAmount > 1) {
            attackString += diceAmount;
        }

        if(enemy.Strength != 0 && diceAmount > 1) {
            attackString += "(";
        }

        attackString += diceType.ToString();

        if(enemy.Strength > 0) {
            attackString += " + " + enemy.Strength;
        } else if(enemy.Strength < 0) {
            attackString += " - " + enemy.Strength;
        }

        if(enemy.Strength != 0 && diceAmount > 1) {
            attackString += ")";
        }

        return attackString;
    }
}
