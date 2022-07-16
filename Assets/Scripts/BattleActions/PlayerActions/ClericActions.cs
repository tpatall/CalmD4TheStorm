using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericActionOne : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public ClericActionOne() {
        ActionText = "DEAL D4 DAMAGE.";
        EnergyCost = 1;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[1];

        for(int i = 0; i < 1; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType.D4);
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        enemy[0].ApplyDamage(numbersRolled[0] + Player.Instance.strength);
    }
}

public class ClericActionTwo : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public ClericActionTwo() {
        ActionText = "BLOCK 2D6.";
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

public class ClericActionThree : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public ClericActionThree() {
        ActionText = "HEAL D6 HEALTH.";
        EnergyCost = 1;
        Target = TargetType.SELF;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[1];

        for(int i = 0; i < 1; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType.D6);
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        Player.Instance.Heal(numbersRolled[0]);
    }
}
public class ClericActionFour : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public ClericActionFour() {
        ActionText = "REDUCE STRENGTH BY 1.";
        EnergyCost = 1;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[1] { 1 };

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }
}

public class ClericActionFive : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }

    public ClericActionFive() {
        ActionText = "REDUCE DICE VALUE.";
        EnergyCost = 1;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        return new int[0];
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }
}
