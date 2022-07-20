using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : PersistentSingleton<PlayerInformation>
{
    [Tooltip("Total hitpoints the player starts with.")]
    [SerializeField] private int startHealth = 20;
    public int PlayerHealth { get; set; }

    [Tooltip("Amount of rerolls a player starts with each encounter.")]
    [SerializeField] private int startRerolls = 2;
    public int MaxRerolls { get; set; }

    [Tooltip("Amount of energy a player starts with each encounter.")]
    [SerializeField] private int startEnergy = 4;
    public int MaxEnergy { get; set; }

    // TODO: Initialize actions in Start() using the in-inspector given values.
    public Warrior Warrior;
    public Rogue Rogue;
    public Mage Mage;
    public Cleric Cleric;

    public List<PlayerAction> warriorActions = new List<PlayerAction>();
    public List<PlayerAction> rogueActions = new List<PlayerAction>();
    public List<PlayerAction> mageActions = new List<PlayerAction>();
    public List<PlayerAction> clericActions = new List<PlayerAction>();

    public int CurrentMoney { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = startHealth;
        MaxRerolls = startRerolls;
        MaxEnergy = startEnergy;

        // Initialize the playerActions per class.
        warriorActions = Warrior.GetActions();
        rogueActions = Rogue.GetActions();
        mageActions = Mage.GetActions();
        clericActions = Cleric.GetActions();
        
        CurrentMoney = 0;
    }

    /// <summary>
    ///     Return a list of upgradeable player actions, along with their character type 
    ///     Is called when the shopLevel is entered.
    /// </summary>
    /// <param name="totalItems">Total items the shop should consist of.</param>
    /// <param name="onePerClass">Whether you can upgrade one action maximally per class.</param>
    /// <returns></returns>
    public List<(CharacterType, PlayerAction)> BuildShop(int totalItems = 4, bool onePerClass = true) {
        List<(CharacterType, PlayerAction)> upgradeableActions = new List<(CharacterType, PlayerAction)>();
        PopulateListByClass(upgradeableActions, CharacterType.WARRIOR, warriorActions);
        PopulateListByClass(upgradeableActions, CharacterType.ROGUE, rogueActions);
        PopulateListByClass(upgradeableActions, CharacterType.MAGE, mageActions);
        PopulateListByClass(upgradeableActions, CharacterType.CLERIC, clericActions);

        List<(CharacterType, PlayerAction)> items = new List<(CharacterType, PlayerAction)>();
        (CharacterType, PlayerAction) upgradeableAction;
        for (int i = 0; i < totalItems; i++) {
            upgradeableAction = GetRandomItemAndRemoveIt(upgradeableActions, onePerClass);
            items.Add(upgradeableAction);
        }

        return items;
    }

    /// <summary>
    ///     Add unupgraded actions to list, sorted by class.
    /// </summary>
    /// <param name="list">List of tuples of CharacterType and other unupgraded actions.</param>
    /// <param name="characterType">Class type.</param>
    /// <param name="actions">List of PlayerActions of that class.</param>
    private void PopulateListByClass(List<(CharacterType, PlayerAction)> list, CharacterType characterType, List<PlayerAction> actions) {
        for (int i = 0; i < actions.Count; i++) {
            if (actions[i].UpgradeType != UpgradeType.NONE) {
                list.Add((characterType, actions[i]));
            }
        }
    }

    /// <summary>
    ///     Get a random item from the list and then remove that item.
    /// </summary>
    /// <param name="list">List of actions, sorted by class type.</param>
    /// <param name="removeClass">If the whole class should be removed from the list after one action is picked.</param>
    /// <returns></returns>
    public (CharacterType, PlayerAction) GetRandomItemAndRemoveIt(List<(CharacterType, PlayerAction)> list, bool removeClass) {
        (CharacterType, PlayerAction) randomItem = list[Random.Range(0, list.Count)];
        
        if (removeClass) {
            int i = 0;
            while (i < list.Count) {
                // Dont increment the index when removing an item, as the list will decrease in size.
                if (list[i].Item1 == randomItem.Item1) {
                    list.Remove(list[i]);
                } else {
                    i++;
                }
            }
        } else {
            list.Remove(randomItem);
        }

        return randomItem;
    }

    public void UpdateCash(int newCurrentMoney) {
        CurrentMoney = newCurrentMoney;
    }

    public void ResetPlayerActions() {
        Destroy(this.gameObject);
    }
}