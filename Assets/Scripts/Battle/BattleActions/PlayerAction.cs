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
    public string ActionName { get; set; }

    public string ActionText { get; set; }

    public int EnergyCost { get; set; }

    public TargetType Target { get; set; }

    private int diceCount;

    public DiceType DiceType { get; set; }

    private DebuffType debuffType;

    private ActionIcon actionType;

    public bool SkipReroll { get; set; } = false;

    public UpgradeType UpgradeType { get; set; }
    public int UpgradeCost { get; set; }

    public PlayerAction(string actionName, int energyCost, TargetType targetType, int diceCount, DiceType diceType, DebuffType debuffType,
        ActionIcon actionType, UpgradeType upgradeType, int upgradeCost) {

        ActionName = actionName;
        EnergyCost = energyCost;
        Target = targetType;
        this.diceCount = diceCount;
        DiceType = diceType;
        this.debuffType = debuffType;
        this.actionType = actionType;

        if (actionType == ActionIcon.BUFF || actionType == ActionIcon.DEBUFF) {
            SkipReroll = true;
        }

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

    /// <summary>
    ///     Build the action text based on the action components.
    /// </summary>
    /// <returns>An action text string.</returns>
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
                if (diceCount == 1) {
                    actionText = string.Format("BLOCK {0} DAMAGE", DiceType.ToString());
                }
                else {
                    actionText = string.Format("BLOCK {0}{1} DAMAGE", diceCount, DiceType.ToString());
                }
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
                if (debuffType == DebuffType.STRENGTH) {
                    actionText = "REDUCE ENEMY STRENGTH BY 1";
                } else {
                    actionText = "REDUCE DICE VALUE";
                }
                break;
            case ActionIcon.HEAL:
                if (diceCount == 1) {
                    actionText = string.Format("HEAL {0} HEALTH", DiceType.ToString());
                } else {
                    actionText = string.Format("HEAL {0}{1} HEALTH", diceCount, DiceType.ToString());
                }
                break;
            default:
                break;
        }

        return actionText;
    }

    /// <summary>
    ///     Get the dice numbers based on number of rolls.
    /// </summary>
    /// <returns>The integer values from the dice rolls, or 0 or 1 if special action.</returns>
    public int[] PrepareAction() {
        // For empty actions.
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

    /// <summary>
    ///     Perform the action on the given targets using the numbers rolled, or change attributes.
    /// </summary>
    /// <param name="targets">Enemy targets affected by this action.</param>
    /// <param name="numbersRolled">Dice numbers based on rolls.</param>
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
                Player.Instance.UpdateStrength();
                break;
            case ActionIcon.DEBUFF:
                if (debuffType == DebuffType.STRENGTH) {
                    targets[0].strengthDebuff += numbersRolled[0];
                } else {
                    targets[0].debuffed = true;
                }

                targets[0].previewText.text = targets[0].readiedAction.GetActionText();
                break;
            case ActionIcon.HEAL:
                Player.Instance.Heal(numbersRolled);
                break;
            default:
                break;
        }
    }

    /// <summary>
    ///     Do an attack on the given targets.
    /// </summary>
    /// <param name="targets">Enemy targets affected by this attack.</param>
    /// <param name="numbersRolled">Dice numbers based on rolls.</param>
    private void DoAttack(Enemy[] targets, int[] numbersRolled) {
        for (int i = 0; i < targets.Length; i++) {
            for (int j = 0; j < numbersRolled.Length; j++) {
                if (targets[i].ApplyDamage(numbersRolled[j] + Player.Instance.Strength)) {
                    break;
                }
            }
        }
    }

    /// <summary>
    ///     Do a block based on the dice rolls.
    /// </summary>
    /// <param name="numbersRolled">Dice numbers based on rolls.</param>
    private void DoBlock(int[] numbersRolled) {
        int totalBlock = 0;
        for (int i = 0; i < numbersRolled.Length; i++) {
            totalBlock += numbersRolled[i];
        }
        Player.Instance.GainBlock(totalBlock);
    }

    /// <summary>
    ///     Do a fake-out upgrade to get the correct text.
    /// </summary>
    /// <returns>The actionText if this action was upgraded.</returns>
    public string GetUpgradeText() {
        string upgradeText = "";

        switch (this.UpgradeType) {
            case UpgradeType.DICECOUNT:
                diceCount++;
                upgradeText = ActionTextBuilder();
                diceCount--;
                break;
            case UpgradeType.DICETYPE:
                DiceType = GetNextDiceType(DiceType);
                upgradeText = ActionTextBuilder();
                DiceType = GetPreviousDiceType(DiceType);
                break;
            case UpgradeType.ENERGYCOST:
                upgradeText = "LOWER ENERGY COST \n\nFROM 2 TO 1";
                break;
            default:
                break;
        }

        // Update action text.
        return upgradeText;
    }

    /// <summary>
    ///     Upgrade this action.
    /// </summary>
    public void Upgrade() {
        switch (this.UpgradeType) {
            case UpgradeType.DICECOUNT:
                diceCount++;
                break;
            // Already takes into account if DiceType is lower than D20
            case UpgradeType.DICETYPE:
                DiceType = GetNextDiceType(DiceType);
                break;
            // Already takes into account if EnergyCost is higher than 1
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

    // Ugly but necessary hardcoded enum iteration to save space elsewhere.
    #region DiceType enum iteration
    private DiceType GetPreviousDiceType(DiceType currentDiceType) {
        DiceType previousDiceType;
        switch (currentDiceType) {
            case DiceType.D6:
                previousDiceType = DiceType.D4;
                break;
            case DiceType.D8:
                previousDiceType = DiceType.D6;
                break;
            case DiceType.D10:
                previousDiceType = DiceType.D8;
                break;
            case DiceType.D12:
                previousDiceType = DiceType.D10;
                break;
            case DiceType.D20:
                previousDiceType = DiceType.D12;
                break;
            default:
                previousDiceType = DiceType.D4;
                break;
        }
        return previousDiceType;
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
    #endregion
}
