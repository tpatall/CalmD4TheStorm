using System;
using UnityEngine;

[Serializable]
public enum TargetType {
    // Hide the SELF targettype in inspector, as it is either implied (BUFF's) or unnecessary (ATTACKs)
    [InspectorName(null)]
    SELF,
    SINGLE,
    ALL
}

[Serializable]
public enum ActionIcon {
    EMPTY,
    ATTACK,
    BLOCK,
    POISON, // DoT
    BUFF,
    DEBUFF,
    HEAL
}

public class PlayerAction {
    public string ActionText { get; set; }

    public int EnergyCost { get; set; }

    public TargetType Target { get; set; }

    public DiceType DiceType { get; set; }

    private int diceCount;

    private ActionIcon actionType;

    public bool SkipReroll { get; set; } = false;

    public UpgradeType UpgradeType { get; set; }
    public int UpgradeCost { get; set; }

    public PlayerAction(int energyCost, TargetType targetType, DiceType diceType, int diceCount, 
        ActionIcon actionType, UpgradeType upgradeType, int upgradeCost) {
        EnergyCost = energyCost;
        Target = targetType;
        DiceType = diceType;
        this.diceCount = diceCount;
        this.actionType = actionType;

        UpgradeType = upgradeType;
        if (upgradeType != UpgradeType.NONE) {
            // Cant upgrade to 0 energy cost
            if (upgradeType == UpgradeType.ENERGYCOST && EnergyCost == 1) {
                UpgradeType = UpgradeType.DICECOUNT;
            }
            // Cant upgrade dicetype to above D20
            else if (upgradeType == UpgradeType.DICETYPE && DiceType == DiceType.D20) {
                UpgradeType = UpgradeType.DICECOUNT;
            }
        }

        UpgradeCost = upgradeCost;

        ActionText = ActionTextBuilder();
    }

    private string ActionTextBuilder() {
        string actionText = "EMPTY";

        switch (actionType) {
            case ActionIcon.ATTACK:
                if (diceCount == 1) {
                    actionText = string.Format("DEAL {0} DAMAGE", DiceType.ToString());
                }
                else {
                    actionText = string.Format("DEAL {0}{1} DAMAGE", diceCount, DiceType.ToString());
                }

                if (Target == TargetType.ALL)
                    actionText += " TO ALL";
                break;
            case ActionIcon.BLOCK:
                actionText = string.Format("BLOCK {0} DAMAGE", DiceType.ToString());
                break;
            case ActionIcon.POISON:
                actionText = string.Format("DEAL {0} DAMAGE", DiceType.ToString());

                if (Target == TargetType.ALL)
                    actionText += " TO ALL";

                actionText += " AND APPLY POISON";
                break;
            case ActionIcon.BUFF:
                actionText = "INCREASE STRENGTH BY 1";
                break;
            case ActionIcon.DEBUFF:
                actionText = "REDUCE ENEMY STRENGTH BY 1";
                break;
            case ActionIcon.HEAL:
                actionText = string.Format("HEAL {0}{1} HEALTH", diceCount, DiceType.ToString());
                break;
            default:
                break;
        }

        return actionText;
    }

    public int[] PrepareAction() {
        if (diceCount < 0) return new int[1] { 0 };
        
        Debug.Log("Preparing action.");

        // Attack, Block and Heal.
        int[] numbersRolled = new int[diceCount];
        for (int i = 0; i < diceCount; i++) {
            numbersRolled[i] = DiceHelper.GetRandomFromDice(DiceType);
        }

        // Buff and Debuff.
        if (diceCount == 0) {
            numbersRolled = new int[1] { 1 };
        }

        return numbersRolled;
    }

    public void DoAction(Enemy[] targets, int[] numbersRolled) {
        switch (actionType) {
            case ActionIcon.ATTACK:
                DoAttack(targets, numbersRolled);
                break;
            case ActionIcon.BLOCK:
                DoBlock(numbersRolled);
                break;
            case ActionIcon.POISON:
                DoAttack(targets, numbersRolled);
                break;
            case ActionIcon.BUFF:
                Player.Instance.strength++;
                break;
            case ActionIcon.DEBUFF:
                targets[0].strengthDebuff += numbersRolled[0];
                targets[0].previewText.text = targets[0].readiedAction.GetActionText();
                break;
            case ActionIcon.HEAL:
                Player.Instance.Heal(numbersRolled[0]);
                break;
            default:
                break;
        }
    }

    private void DoAttack(Enemy[] targets, int[] numbersRolled) {
        for (int i = 0; i < targets.Length; i++) {
            for (int j = 0; j < numbersRolled.Length; j++) {
                if (targets[i].ApplyDamage(numbersRolled[j] + Player.Instance.strength)) {
                    break;
                }
            }
        }
    }

    private void DoBlock(int[] numbersRolled) {
        int totalBlock = 0;
        for (int i = 0; i < numbersRolled.Length; i++) {
            totalBlock += numbersRolled[i];
        }
        Player.Instance.GainBlock(totalBlock);
    }

    public void Upgrade() {
        switch (this.UpgradeType) {
            case UpgradeType.DICECOUNT:
                diceCount++;
                break;
            case UpgradeType.DICETYPE:
                DiceType = GetNextDiceType(DiceType);
                break;
            case UpgradeType.ENERGYCOST:
                EnergyCost--;
                break;
            default:
                break;
        }

        // Update action text.
        ActionText = ActionTextBuilder();
        // No longer upgradeable now.
        UpgradeType = UpgradeType.NONE;
    }

    private DiceType GetNextDiceType(DiceType currentDiceType) {
        DiceType nextDiceType;
        switch (currentDiceType) {
            case DiceType.D4:
                nextDiceType = DiceType.D6;
                break;
            case DiceType.D6:
                nextDiceType = DiceType.D8;
                break;
            case DiceType.D8:
                nextDiceType = DiceType.D10;
                break;
            case DiceType.D10:
                nextDiceType = DiceType.D12;
                break;
            case DiceType.D12:
                nextDiceType = DiceType.D20;
                break;
            default:
                nextDiceType = DiceType.D4;
                break;
        }
        return nextDiceType;
    }
}

//public interface PlayerAction
//{
//    public int EnergyCost { get; set; }

//    public TargetType Target { get; set; }

//    public string ActionText { get; set; }

//    public bool SkipReroll { get; set; }

//    public DiceType DiceType { get; set; }

//    public bool Upgraded { get; set; }

//    public int[] PrepareAction();

//    public void DoAction(Enemy[] targets, int[] numbersRolled);

//    public void Upgrade();
//}
