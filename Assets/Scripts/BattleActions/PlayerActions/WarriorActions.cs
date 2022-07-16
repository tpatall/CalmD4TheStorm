using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorActionOne : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public WarriorActionOne() {
        ActionText = "DEAL 3D4 DAMAGE.";
        EnergyCost = 2;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[3];

        for(int i = 0; i < 3; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType.D4);
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        for(int i = 0; i < enemy.Length; i++) {
            for(int j = 0; j < numbersRolled.Length; j++) {
                if(enemy[i].ApplyDamage(numbersRolled[j] + Player.Instance.strength)) {
                    break;
                }
            }
        }
    }
}

public class WarriorActionTwo : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public WarriorActionTwo() {
        ActionText = "BLOCK 3D6.";
        EnergyCost = 1;
        Target = TargetType.SELF;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[3];

        for(int i = 0; i < 3; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType.D6);
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        int totalBlock = 0;
        for(int i = 0; i < numbersRolled.Length; i++) {
            totalBlock += numbersRolled[i];
        }
        Player.Instance.GainBlock(totalBlock);
    }
}

public class WarriorActionThree : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public WarriorActionThree() {
        ActionText = "INCREASE STRENGTH BY 1.";
        EnergyCost = 1;
        Target = TargetType.SELF;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        return new int[1] { 1 };
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }
}
public class WarriorActionFour : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public WarriorActionFour() {
        ActionText = "EMPTY.";
        EnergyCost = 1;
        Target = TargetType.ALL;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        return new int[0];
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }
}

public class WarriorActionFive : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public WarriorActionFive() {
        ActionText = "EMPTY.";
        EnergyCost = 1;
        Target = TargetType.ALL;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        return new int[0];
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }
}
