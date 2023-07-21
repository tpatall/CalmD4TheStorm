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
public enum DebuffType
{
    STRENGTH,
    DICETYPE
}

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

    [Range(1, 10)]
    public int DiceCount = 1;
    
    public DiceType DiceType = DiceType.D4;

    public DebuffType DebuffType = DebuffType.STRENGTH;

    public UpgradeType UpgradeType = UpgradeType.DICECOUNT;

    [Range(1, 5)]
    public int UpgradeCost = 1;

    private string ActionName;

    public ActionInformation(string actionName) {
        ActionName = actionName;
    }

    public PlayerAction InitializePlayerAction() {
        PlayerAction playerAction;

        switch (ActionType) {
            case ActionIcon.EMPTY:
                playerAction = new PlayerAction(ActionName, 0, TargetType.ALL, -1, DiceType.D4, DebuffType.STRENGTH, ActionIcon.EMPTY, UpgradeType.NONE, 0);
                break;
            case ActionIcon.ATTACK:
                playerAction = new PlayerAction(ActionName, EnergyCost, TargetType, 
                    DiceCount, DiceType,
                    DebuffType.STRENGTH, ActionIcon.ATTACK, 
                    UpgradeType, UpgradeCost);
                break;
            case ActionIcon.BLOCK:
                playerAction = new PlayerAction(ActionName, EnergyCost, TargetType.SELF, 
                    DiceCount, DiceType,
                    DebuffType.STRENGTH, ActionIcon.BLOCK, 
                    UpgradeType, UpgradeCost);
                break;
            case ActionIcon.POISON:
                playerAction = new PlayerAction(ActionName, EnergyCost, TargetType.SELF, 
                    DiceCount, DiceType,
                    DebuffType.STRENGTH, ActionIcon.POISON, 
                    UpgradeType, UpgradeCost);
                break;
            case ActionIcon.BUFF:
                playerAction = new PlayerAction(ActionName, EnergyCost, TargetType.SELF, 
                    0, DiceType.D4,
                    DebuffType.STRENGTH, ActionIcon.BUFF, 
                    UpgradeType.NONE, 0);
                break;
            case ActionIcon.DEBUFF:
                playerAction = new PlayerAction(ActionName, EnergyCost, TargetType, 
                    0, DiceType.D4,
                    DebuffType, ActionIcon.DEBUFF, 
                    UpgradeType.NONE, 0);
                break;
            case ActionIcon.HEAL:
                playerAction = new PlayerAction(ActionName, EnergyCost, TargetType.SELF, 
                    DiceCount, DiceType,
                    DebuffType.STRENGTH, ActionIcon.HEAL, 
                    UpgradeType, UpgradeCost);
                break;
            default:
                playerAction = new PlayerAction(ActionName, 0, TargetType.SELF, -1, DiceType.D4, DebuffType.STRENGTH, ActionIcon.EMPTY, UpgradeType.NONE, 0);
                break;
        }

        return playerAction;
    }
}
