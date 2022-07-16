using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueActionOne : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public RogueActionOne() {
        ActionText = "Deal d12 damage.";
        EnergyCost = 1;
        RequiresTarget = true;
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
        
    }
}

public class RogueActionTwo : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public RogueActionTwo() {
        ActionText = "Block d6.";
        EnergyCost = 1;
        RequiresTarget = false;
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

    }
}

public class RogueActionThree : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public RogueActionThree() {
        ActionText = "Deal d4 damage and apply d6 poison.";
        EnergyCost = 1;
        RequiresTarget = true;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        int[] numbersRolled = new int[2];

        numbersRolled[0] = DiceHelper.GetRandomFromDice(DiceType.D4);
        numbersRolled[1] = DiceHelper.GetRandomFromDice(DiceType.D6);

        return numbersRolled;
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }
}
public class RogueActionFour : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public RogueActionFour() {
        ActionText = "Empty.";
        EnergyCost = 1;
        RequiresTarget = true;
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
    public bool RequiresTarget { get; set; }

    public RogueActionFive() {
        ActionText = "Empty.";
        EnergyCost = 1;
        RequiresTarget = true;
    }

    public int[] PrepareAction() {
        Debug.Log("Performing action");

        return new int[0];
    }

    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

    }
}
