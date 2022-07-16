using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageActionOne : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public MageActionOne() {
        ActionText = "Deal 2d4 damage to all dicemies.";
        EnergyCost = 1;
        RequiresTarget = false;
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
        
    }
}

public class MageActionTwo : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public MageActionTwo() {
        ActionText = "Block 2d6.";
        EnergyCost = 1;
        RequiresTarget = false;
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

    }
}

public class MageActionThree : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public MageActionThree() {
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
public class MageActionFour : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public MageActionFour() {
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

public class MageActionFive : PlayerAction {
    public string ActionText { get; set; }
    public int EnergyCost { get; set; }
    public bool RequiresTarget { get; set; }

    public MageActionFive() {
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
