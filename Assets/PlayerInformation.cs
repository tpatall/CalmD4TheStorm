using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] private int startHealth = 20;
    public int PlayerHealth { get; set; }

    // TODO: Initialize actions in Start() using the in-inspector given values.
    public Warrior Warrior;
    public Rogue Rogue;
    public Mage Mage;
    public Cleric Cleric;

    public List<PlayerAction> warriorActions = new List<PlayerAction>();
    public List<PlayerAction> rogueActions = new List<PlayerAction>();
    public List<PlayerAction> mageActions = new List<PlayerAction>();
    public List<PlayerAction> clericActions = new List<PlayerAction>();

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = startHealth;

        // Initialize the playerActions per class.
        warriorActions = Warrior.GetActions();
        rogueActions = Rogue.GetActions();
        mageActions = Mage.GetActions();
        clericActions = Cleric.GetActions();
    }

    // Call after loading in shop scene.
    public List<(PlayerAction, int, string)> BuildShop() {
        List<(PlayerAction, int)> upgradeableActions = new List<(PlayerAction, int)>();

        // Get 2 unupgraded actions.
        int list;
        PlayerAction playerAction;

        List<PlayerAction> warriorActionsCopy = warriorActions;
        List<PlayerAction> rogueActionsCopy = rogueActions;
        List<PlayerAction> mageActionsCopy = mageActions;
        List<PlayerAction> clericActionsCopy = clericActions;
        for (int i = 0; i < 2; i++) {
            list = Random.Range(0, 4);
            if (list == 0) {
                playerAction = GetRandomAction(warriorActionsCopy);
                if (playerAction != null) {
                    upgradeableActions.Add((playerAction, list));
                }

            } else if (list == 1) {
                playerAction = GetRandomAction(rogueActionsCopy);
                if (playerAction != null) {
                    upgradeableActions.Add((playerAction, list));
                }

            } else if (list == 2) {
                playerAction = GetRandomAction(mageActionsCopy);
                if (playerAction != null) {
                    upgradeableActions.Add((playerAction, list));
                }

            } else {
                playerAction = GetRandomAction(clericActionsCopy);
                if (playerAction != null) {
                    upgradeableActions.Add((playerAction, list));
                }
            }
        }

        List<(PlayerAction, int, string)> shopItems = new List<(PlayerAction, int, string)>();
        for (int i = 0; i < upgradeableActions.Count; i++) {
            PlayerAction copyAction = upgradeableActions[i].Item1;
            copyAction.Upgrade();
            shopItems.Add((upgradeableActions[i].Item1, upgradeableActions[i].Item2, copyAction.ActionText));
        }

        return shopItems;
    }

    public PlayerAction GetRandomAction(List<PlayerAction> playerActions) {
        PlayerAction notUpgradedPlayerAction = GetRandomItemAndRemoveIt(playerActions);
        while (playerActions.Count > 0 && notUpgradedPlayerAction.UpgradeType != UpgradeType.NONE) {
            notUpgradedPlayerAction = GetRandomItemAndRemoveIt(playerActions);
        }

        if (notUpgradedPlayerAction.UpgradeType != UpgradeType.NONE) {
            return notUpgradedPlayerAction;
        } else {
            return null;
        }
    }

    public PlayerAction GetRandomItemAndRemoveIt(List<PlayerAction> list) {
        PlayerAction randomItem = list[Random.Range(0, list.Count)];
        list.Remove(randomItem);
        return randomItem;
    }

    public void ResetPlayerActions() {
        Destroy(this.gameObject);
    }
}