using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour, BattleAction {

    Enemy enemy;
    public DiceType diceType;
    public int diceAmount;

    void Start() {
        enemy = GetComponent<Enemy>();
    }

    public void DoAction() {
        Debug.Log(enemy.name + " is dealing " + diceAmount + "(" + diceType + " + " + enemy.Strength + ") damage!");

        for(int i = 0; i < diceAmount; i++) {
            int damage = DiceHelper.GetRandomFromDice(diceType) + enemy.Strength;
            Debug.Log("Hit for " + damage + " damage!");
        }
    }

    public string GetActionIcon() {
        return "attackIcon.png";
    }

    public string GetActionText() {
        string attackString = "";
        
        if(diceAmount > 1) {
            attackString += diceAmount + "(";
        }

        attackString += diceType.ToString();

        if(enemy.Strength > 0) {
            attackString += " + " + enemy.Strength;
        } else if(enemy.Strength < 0) {
            attackString += " - " + enemy.Strength;
        }

        if(diceAmount > 1) {
            attackString += diceAmount + ")";
        }

        return attackString;
    }
}
