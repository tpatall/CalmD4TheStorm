using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Interface to possibly extend in future.
/// </summary>
public interface CharacterClass
{
    public CharacterType CharacterType { get; set; }

    public List<PlayerAction> GetActions();
}

[System.Serializable]
public enum CharacterType
{
    BLANK,
    WARRIOR,
    ROGUE,
    MAGE,
    CLERIC
}

[System.Serializable]
public class Warrior : CharacterClass
{
    public CharacterType CharacterType { get; set; } = CharacterType.WARRIOR;

    public ActionInformation action1 = new ActionInformation("WARRIOR - ACTION 1");
    public ActionInformation action2 = new ActionInformation("WARRIOR - ACTION 2");
    public ActionInformation action3 = new ActionInformation("WARRIOR - ACTION 3");
    public ActionInformation action4 = new ActionInformation("WARRIOR - ACTION 4");
    public ActionInformation action5 = new ActionInformation("WARRIOR - ACTION 5");

    public List<PlayerAction> GetActions() {
        List<PlayerAction> list = new List<PlayerAction>();
        PlayerAction playerAction = action1.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action2.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action3.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action4.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action5.InitializePlayerAction();
        list.Add(playerAction);

        return list;
    }
}

[System.Serializable]
public class Rogue : CharacterClass
{
    public CharacterType CharacterType { get; set; } = CharacterType.ROGUE;

    public ActionInformation action1 = new ActionInformation("ROGUE - ACTION 1");
    public ActionInformation action2 = new ActionInformation("ROGUE - ACTION 2");
    public ActionInformation action3 = new ActionInformation("ROGUE - ACTION 3");
    public ActionInformation action4 = new ActionInformation("ROGUE - ACTION 4");
    public ActionInformation action5 = new ActionInformation("ROGUE - ACTION 5");

    public List<PlayerAction> GetActions() {
        List<PlayerAction> list = new List<PlayerAction>();
        PlayerAction playerAction = action1.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action2.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action3.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action4.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action5.InitializePlayerAction();
        list.Add(playerAction);

        return list;
    }
}

[System.Serializable]
public class Mage : CharacterClass
{
    public CharacterType CharacterType { get; set; } = CharacterType.MAGE;

    public ActionInformation action1 = new ActionInformation("MAGE - ACTION 1");
    public ActionInformation action2 = new ActionInformation("MAGE - ACTION 2");
    public ActionInformation action3 = new ActionInformation("MAGE - ACTION 3");
    public ActionInformation action4 = new ActionInformation("MAGE - ACTION 4");
    public ActionInformation action5 = new ActionInformation("MAGE - ACTION 5");

    public List<PlayerAction> GetActions() {
        List<PlayerAction> list = new List<PlayerAction>();
        PlayerAction playerAction = action1.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action2.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action3.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action4.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action5.InitializePlayerAction();
        list.Add(playerAction);

        return list;
    }
}

[System.Serializable]
public class Cleric : CharacterClass
{
    public CharacterType CharacterType { get; set; } = CharacterType.CLERIC;

    public ActionInformation action1 = new ActionInformation("CLERIC - ACTION 1");
    public ActionInformation action2 = new ActionInformation("CLERIC - ACTION 2");
    public ActionInformation action3 = new ActionInformation("CLERIC - ACTION 3");
    public ActionInformation action4 = new ActionInformation("CLERIC - ACTION 4");
    public ActionInformation action5 = new ActionInformation("CLERIC - ACTION 5");

    public List<PlayerAction> GetActions() {
        List<PlayerAction> list = new List<PlayerAction>();
        PlayerAction playerAction = action1.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action2.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action3.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action4.InitializePlayerAction();
        list.Add(playerAction);

        playerAction = action5.InitializePlayerAction();
        list.Add(playerAction);

        return list;
    }
}
