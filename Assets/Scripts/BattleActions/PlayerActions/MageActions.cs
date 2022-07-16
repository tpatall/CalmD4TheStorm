using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageActionOne : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public MageActionOne() {
        ActionText = "DEAL 2D4 DAMAGE TO ALL";
        EnergyCost = 1;
        Target = TargetType.ALL;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[2];

        for(int i = 0; i < 2; i++) {
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

public class MageActionTwo : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public MageActionTwo() {
        ActionText = "BLOCK 2D6";
        EnergyCost = 1;
        Target = TargetType.SELF;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[2];

        for(int i = 0; i < 2; i++) {
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

public class MageActionThree : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public MageActionThree() {
        ActionText = "EMPTY";
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
public class MageActionFour : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public MageActionFour() {
        ActionText = "EMPTY";
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

public class MageActionFive : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public MageActionFive() {
        ActionText = "EMPTY";
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
