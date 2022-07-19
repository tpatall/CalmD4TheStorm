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
public class Warrior : CharacterClass
{
    public CharacterType CharacterType { get; set; } = CharacterType.WARRIOR;

    public ActionInformation action1 = new ActionInformation();
    public ActionInformation action2 = new ActionInformation();
    public ActionInformation action3 = new ActionInformation();
    public ActionInformation action4 = new ActionInformation();
    public ActionInformation action5 = new ActionInformation();

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

    public ActionInformation action1 = new ActionInformation();
    public ActionInformation action2 = new ActionInformation();
    public ActionInformation action3 = new ActionInformation();
    public ActionInformation action4 = new ActionInformation();
    public ActionInformation action5 = new ActionInformation();

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

    public ActionInformation action1 = new ActionInformation();
    public ActionInformation action2 = new ActionInformation();
    public ActionInformation action3 = new ActionInformation();
    public ActionInformation action4 = new ActionInformation();
    public ActionInformation action5 = new ActionInformation();

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

    public ActionInformation action1 = new ActionInformation();
    public ActionInformation action2 = new ActionInformation();
    public ActionInformation action3 = new ActionInformation();
    public ActionInformation action4 = new ActionInformation();
    public ActionInformation action5 = new ActionInformation();

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
