using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    List<PlayerAction> warriorActions = new List<PlayerAction>();
    List<PlayerAction> rogueActions = new List<PlayerAction>();
    List<PlayerAction> mageActions = new List<PlayerAction>();
    List<PlayerAction> clericActions = new List<PlayerAction>();

    public int PlayerHealth { get; set; }

    public int Pips { get; set; }

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 20;
        Pips = 0;

        // Create all actions.
        warriorActions.Add(new WarriorActionOne());
        warriorActions.Add(new WarriorActionTwo());
        warriorActions.Add(new WarriorActionThree());
        warriorActions.Add(new WarriorActionFour());
        warriorActions.Add(new WarriorActionFive());

        rogueActions.Add(new RogueActionOne());
        rogueActions.Add(new RogueActionTwo());
        rogueActions.Add(new RogueActionThree());
        rogueActions.Add(new RogueActionFour());
        rogueActions.Add(new RogueActionFive());

        mageActions.Add(new MageActionOne());
        mageActions.Add(new MageActionTwo());
        mageActions.Add(new MageActionThree());
        mageActions.Add(new MageActionFour());
        mageActions.Add(new MageActionFive());

        clericActions.Add(new ClericActionOne());
        clericActions.Add(new ClericActionTwo());
        clericActions.Add(new ClericActionThree());
        clericActions.Add(new ClericActionFour());
        clericActions.Add(new ClericActionFive());
    }

    // Call after loading in battle scene.
    public void GiveInformation() {
        FindObjectOfType<PlayerActionController>().SetUpActions(warriorActions, rogueActions, mageActions, clericActions);
        FindObjectOfType<Player>().SetUpHealth(PlayerHealth);
    }

    // Call after loading in shop scene.
    public List<PlayerAction> BuildShop() {
        List<PlayerAction> upgradeableActions = new List<PlayerAction>();

        // Get 2 unupgraded actions.
        int list;
        PlayerAction playerAction;
        for (int i = 0; i < 2; i++) {
            list = Random.Range(0, 4);
            if (list == 0) {
                playerAction = GetRandomAction(warriorActions);
                if (playerAction != null) {
                    upgradeableActions.Add(playerAction);
                } else {
                    i--;
                }

            } else if (list == 1) {
                playerAction = GetRandomAction(rogueActions);
                if (playerAction != null) {
                    upgradeableActions.Add(playerAction);
                }
                else {
                    i--;
                }

            } else if (list == 2) {
                playerAction = GetRandomAction(mageActions);
                if (playerAction != null) {
                    upgradeableActions.Add(playerAction);
                }
                else {
                    i--;
                }

            } else {
                playerAction = GetRandomAction(clericActions);
                if (playerAction != null) {
                    upgradeableActions.Add(playerAction);
                }
                else {
                    i--;
                }
            }
        }

        return upgradeableActions;
    }

    public PlayerAction GetRandomAction(List<PlayerAction> actions) {
        List<PlayerAction> playerActions = actions;
        PlayerAction notUpgradedPlayerAction = GetRandomItemAndRemoveIt(playerActions);
        while (playerActions.Count > 0 && !notUpgradedPlayerAction.Upgraded) {
            notUpgradedPlayerAction = GetRandomItemAndRemoveIt(playerActions);
        }

        if (notUpgradedPlayerAction.Upgraded) {
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
