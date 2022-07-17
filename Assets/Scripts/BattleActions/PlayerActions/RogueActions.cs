using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueActionOne : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }


    public RogueActionOne() {
        ActionText = "DEAL D12 DAMAGE";
        EnergyCost = 1;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[1];

        for(int i = 0; i < 1; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType.D12);
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        enemy[0].ApplyDamage(numbersRolled[0] + Player.Instance.strength);
    }
}

public class RogueActionTwo : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }


    public RogueActionTwo() {
        ActionText = "BLOCK D6";
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
        Player.Instance.GainBlock(numbersRolled[0]);
    }
}

public class RogueActionThree : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }


    public RogueActionThree() {
        ActionText = "DEAL D4 DAMAGE APPLY D6 POISON";
        EnergyCost = 1;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[2];

        numbersRolled[0] = DiceHelper.GetRandomFromDice(DiceType.D4);
        numbersRolled[1] = DiceHelper.GetRandomFromDice(DiceType.D6);

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        enemy[0].ApplyDamage(numbersRolled[0] + Player.Instance.strength);
    }
}
public class RogueActionFour : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }


    public RogueActionFour() {
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

public class RogueActionFive : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }


    public RogueActionFive() {
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
