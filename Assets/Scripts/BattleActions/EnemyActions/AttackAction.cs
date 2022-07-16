using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour, BattleAction {

    public Enemy enemy;
    public DiceType diceType;
    public int diceAmount;

    public void DoAction() {
        Debug.Log(enemy.name + " is dealing " + diceAmount + "(" + diceType + " + " + enemy.Strength + ") damage!");

        DiceType damageDiceType = diceType;

        if(enemy.debuffed && damageDiceType > 0) {
            damageDiceType--;
        }

        for(int i = 0; i < diceAmount; i++) {
            int damage = DiceHelper.GetRandomFromDice(damageDiceType) + enemy.Strength;

            Player.Instance.TakeDamage(damage);

            Debug.Log("Hit for " + damage + " damage!");
        }
    }

    public string GetActionIcon() {
        return "attackIcon";
    }

    public string GetActionText() {
        string attackString = "";

        DiceType textDiceType = diceType;

        if(enemy.debuffed && textDiceType > 0) {
            textDiceType--;
        }

        if(diceAmount > 1) {
            attackString += diceAmount;
        }

        if(enemy.Strength != 0 && diceAmount > 1) {
            attackString += "(";
        }

        attackString += textDiceType.ToString();

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
