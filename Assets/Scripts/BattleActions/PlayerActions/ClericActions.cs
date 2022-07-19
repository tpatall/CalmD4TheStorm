//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ClericActionOne : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = false;

//    public ClericActionOne() {
//        ActionText = "DEAL D4 DAMAGE";
//        DiceType = DiceType.D4;
//        EnergyCost = 1;
//        Target = TargetType.SINGLE;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        int[] numbersRolled = new int[1];

//        for(int i = 0; i < 1; i++) {
//            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType);
//        }

//        return numbersRolled;
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
//        enemy[0].ApplyDamage(numbersRolled[0] + Player.Instance.strength);
//    }

//    public void Upgrade() {
//        Upgraded = true;

//        ActionText = "DEAL D6 DAMAGE";
//        DiceType = DiceType.D6;
//    }
//}

//public class ClericActionTwo : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = false;

//    public ClericActionTwo() {
//        ActionText = "BLOCK 2D6";
//        DiceType = DiceType.D6;
//        EnergyCost = 1;
//        Target = TargetType.SELF;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        int[] numbersRolled = new int[2];

//        for(int i = 0; i < 2; i++) {
//            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType);
//        }

//        return numbersRolled;
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
//        int totalBlock = 0;
//        for(int i = 0; i < numbersRolled.Length; i++) {
//            totalBlock += numbersRolled[i];
//        }
//        Player.Instance.GainBlock(totalBlock);
//    }

//    public void Upgrade() {
//        Upgraded = true;

//        ActionText = "BLOCK 2D8";
//        DiceType = DiceType.D8;
//    }
//}

//public class ClericActionThree : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = false;

//    public ClericActionThree() {
//        ActionText = "HEAL D6 HEALTH";
//        DiceType = DiceType.D6;
//        EnergyCost = 1;
//        Target = TargetType.SELF;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        int[] numbersRolled = new int[1];

//        for(int i = 0; i < 1; i++) {
//            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType);
//        }

//        return numbersRolled;
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
//        Player.Instance.Heal(numbersRolled[0]);
//    }

//    public void Upgrade() {
//        Upgraded = true;

//        ActionText = "BLOCK 2D8";
//        DiceType = DiceType.D8;
//    }
//}

//public class ClericActionFour : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = true;

//    public ClericActionFour() {
//        ActionText = "REDUCE STRENGTH BY 1";
//        EnergyCost = 1;
//        Target = TargetType.SINGLE;
//        SkipReroll = true;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        int[] numbersRolled = new int[1] { 1 };

//        return numbersRolled;
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
//        enemy[0].strengthDebuff += numbersRolled[0];

//        enemy[0].previewText.text = enemy[0].readiedAction.GetActionText();
//    }

//    public void Upgrade() {

//    }
//}

//public class ClericActionFive : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = true;

//    public ClericActionFive() {
//        ActionText = "REDUCE DICE VALUE";
//        EnergyCost = 1;
//        Target = TargetType.SINGLE;
//        SkipReroll = true;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        return new int[0];
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {
//        enemy[0].debuffed = true;

//        enemy[0].previewText.text = enemy[0].readiedAction.GetActionText();
//    }

//    public void Upgrade() {

//    }
//}
