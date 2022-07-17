using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueActionOne : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }

    public DiceType DiceType { get; set; }
    public bool Upgraded { get; set; } = false;

    public RogueActionOne() {
        ActionText = "DEAL D12 DAMAGE";
        DiceType = DiceType.D12;
        EnergyCost = 1;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[1];

        for(int i = 0; i < 1; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType);
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        enemy[0].ApplyDamage(numbersRolled[0] + Player.Instance.strength);
    }

    public void Upgrade() {
        Upgraded = true;

        ActionText = "DEAL D20 DAMAGE";
        DiceType = DiceType.D20;
    }
}

public class RogueActionTwo : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }

    public DiceType DiceType { get; set; }
    public bool Upgraded { get; set; } = false;

    public RogueActionTwo() {
        ActionText = "BLOCK D6";
        DiceType = DiceType.D6;
        EnergyCost = 1;
        Target = TargetType.SELF;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[1];

        for(int i = 0; i < 1; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType);
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        Player.Instance.GainBlock(numbersRolled[0]);
    }

    public void Upgrade() {
        Upgraded = true;

        ActionText = "BLOCK D8";
        DiceType = DiceType.D8;
    }
}

public class RogueActionThree : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }

    public DiceType DiceType { get; set; }
    public bool Upgraded { get; set; } = false;

    public RogueActionThree() {
        ActionText = "DEAL D4 DAMAGE APPLY D6 POISON";
        DiceType = DiceType.D4;
        EnergyCost = 1;
        Target = TargetType.SINGLE;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[2];

        numbersRolled[0] = DiceHelper.GetRandomFromDice(DiceType);
        numbersRolled[1] = DiceHelper.GetRandomFromDice(DiceType.D6);

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
        enemy[0].ApplyDamage(numbersRolled[0] + Player.Instance.strength);
    }

    public void Upgrade() {
        Upgraded = true;

        ActionText = "DEAL D6 DAMAGE APPLY D6 POISON";
        DiceType = DiceType.D4;
    }
}

public class RogueActionFour : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }

    public DiceType DiceType { get; set; }
    public bool Upgraded { get; set; } = true;

    public RogueActionFour() {
        ActionText = "EMPTY";
        DiceType = DiceType.D4;
        EnergyCost = 1;
        Target = TargetType.ALL;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        return new int[0];
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }

    public void Upgrade() {

    }
}

public class RogueActionFive : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public TargetType Target { get; set; }
    public bool SkipReroll { get; set; }

    public DiceType DiceType { get; set; }
    public bool Upgraded { get; set; } = true;

    public RogueActionFive() {
        ActionText = "EMPTY";
        DiceType = DiceType.D4;
        EnergyCost = 1;
        Target = TargetType.ALL;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        return new int[0];
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }

    public void Upgrade() {

    }
}
