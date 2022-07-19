//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MageActionOne : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = false;

//    public MageActionOne() {
//        ActionText = "DEAL 2D4 DAMAGE TO ALL";
//        DiceType = DiceType.D4;
//        EnergyCost = 1;
//        Target = TargetType.ALL;
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
//        for(int i = 0; i < enemy.Length; i++) {
//            for(int j = 0; j < numbersRolled.Length; j++) {
//                if(enemy[i].ApplyDamage(numbersRolled[j] + Player.Instance.strength)) {
//                    break;
//                }
//            }
//        }
//    }

//    public void Upgrade() {
//        Upgraded = true;

//        ActionText = "DEAL 2D6 DAMAGE TO ALL";
//        DiceType = DiceType.D6;
//    }
//}

//public class MageActionTwo : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = false;

//    public MageActionTwo() {
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

//public class MageActionThree : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = true;

//    public MageActionThree() {
//        ActionText = "EMPTY";
//        DiceType = DiceType.D4;
//        EnergyCost = 1;
//        Target = TargetType.ALL;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        return new int[0];
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

//    }

//    public void Upgrade() {

//    }
//}

//public class MageActionFour : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = true;

//    public MageActionFour() {
//        ActionText = "EMPTY";
//        DiceType = DiceType.D4;
//        EnergyCost = 1;
//        Target = TargetType.ALL;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        return new int[0];
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

//    }

//    public void Upgrade() {

//    }
//}

//public class MageActionFive : PlayerAction {
//    public string ActionText { get; set; }
//    public int EnergyCost { get; set; }
//    public TargetType Target { get; set; }
//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }
//    public bool Upgraded { get; set; } = true;

//    public MageActionFive() {
//        ActionText = "EMPTY";
//        DiceType = DiceType.D4;
//        EnergyCost = 1;
//        Target = TargetType.ALL;
//    }

//    public int[] PrepareAction() {
//        Debug.Log("Performing action");

//        return new int[0];
//    }

//    public void DoAction(Enemy[] enemy, int[] numbersRolled) {

//    }

//    public void Upgrade() {

//    }
//}
