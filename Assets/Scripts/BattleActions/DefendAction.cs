using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendAction : MonoBehaviour, BattleAction {

    Enemy enemy;
    public DiceType diceType;
    public int diceAmount;

    public void DoAction() {
        Debug.Log("Blocking");
    }

    public string GetActionIcon() {
        throw new System.NotImplementedException();
    }

    public string GetActionText() {
        string defendString = "";

        if(diceAmount > 0) {
            defendString += diceAmount + "(";
        }

        defendString += diceType.ToString();

        if(enemy.Dexterity > 0) {
            defendString += " + " + enemy.Strength;
        } else if(enemy.Dexterity < 0) {
            defendString += " - " + enemy.Strength;
        }

        if(diceAmount > 0) {
            defendString += diceAmount + ")";
        }

        return defendString;
    }
}
