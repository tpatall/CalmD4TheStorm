using UnityEngine;

// Attack 1 enemy           once
// Attack 1 enemy           multiple times
// Attack multiple enemies  once
// Attack multiple enemies  multiple times

// Block once
// Block multiple times

// Attack 1 enemy           once and poison
// Attack multiple enemies  once and poison

// Buff (strength)

// Debuff 1 enemy
// Debuff multiple enemies

// Heal once
// Heal multiple times

[System.Serializable]
public enum UpgradeType {
    [InspectorName(null)]
    NONE,
    DICECOUNT,
    DICETYPE,
    ENERGYCOST
}

[System.Serializable]
public class ActionInformation
{
    public ActionIcon ActionType = ActionIcon.EMPTY;

    [Range(1, 2)]
    public int EnergyCost = 1;

    public TargetType TargetType = TargetType.SINGLE;

    public DiceType DiceType = DiceType.D4;

    [Range(1, 10)]
    public int DiceCount = 1;

    public UpgradeType UpgradeType = UpgradeType.DICECOUNT;

    [Range(1, 5)]
    public int UpgradeCost = 1;

    public PlayerAction InitializePlayerAction() {
        PlayerAction playerAction;

        switch (ActionType) {
            case ActionIcon.EMPTY:
                playerAction = new PlayerAction(0, TargetType.ALL, DiceType.D4, -1, ActionIcon.EMPTY, UpgradeType.NONE, 0);
                break;
            case ActionIcon.ATTACK:
                playerAction = new PlayerAction(EnergyCost, TargetType, DiceType, DiceCount, ActionIcon.ATTACK, UpgradeType, UpgradeCost);
                break;
            case ActionIcon.BLOCK:
                playerAction = new PlayerAction(EnergyCost, TargetType.SELF, DiceType, DiceCount, ActionIcon.BLOCK, UpgradeType, UpgradeCost);
                break;
            case ActionIcon.POISON:
                playerAction = new PlayerAction(EnergyCost, TargetType.SELF, DiceType, DiceCount, ActionIcon.POISON, UpgradeType, UpgradeCost);
                break;
            case ActionIcon.BUFF:
                playerAction = new PlayerAction(EnergyCost, TargetType.SELF, DiceType.D4, 0, ActionIcon.BUFF, UpgradeType.NONE, 0);
                break;
            case ActionIcon.DEBUFF:
                playerAction = new PlayerAction(EnergyCost, TargetType, DiceType.D4, 0, ActionIcon.DEBUFF, UpgradeType.NONE, 0);
                break;
            case ActionIcon.HEAL:
                playerAction = new PlayerAction(EnergyCost, TargetType.SELF, DiceType, DiceCount, ActionIcon.HEAL, UpgradeType, UpgradeCost);
                break;
            default:
                playerAction = new PlayerAction(0, TargetType.SELF, DiceType.D4, -1, ActionIcon.EMPTY, UpgradeType.NONE, 0);
                break;
        }

        return playerAction;
    }
}
