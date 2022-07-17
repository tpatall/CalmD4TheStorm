using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour, BattleAction {

    public Enemy enemy;
    public DiceType diceType;
    public int diceAmount;

    public void DoAction() {
        DiceType damageDiceType = diceType;

        if(enemy.debuffed && damageDiceType > 0) {
            damageDiceType--;
        }

        for(int i = 0; i < diceAmount; i++) {
            int damage = DiceHelper.GetRandomFromDice(damageDiceType) + enemy.Strength - enemy.strengthDebuff;

            if(Player.Instance.TakeDamage(damage)) {
                return;
            }
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

        if(enemy.Strength - enemy.strengthDebuff != 0 && diceAmount > 1) {
            attackString += "(";
        }

        attackString += textDiceType.ToString();

        if(enemy.Strength - enemy.strengthDebuff > 0) {
            attackString += " + " + (enemy.Strength - enemy.strengthDebuff);
        } else if(enemy.Strength - enemy.strengthDebuff < 0) {
            attackString += " - " + Mathf.Abs(enemy.Strength - enemy.strengthDebuff);
        }

        if(enemy.Strength - enemy.strengthDebuff != 0 && diceAmount > 1) {
            attackString += ")";
        }

        return attackString;
    }
}
