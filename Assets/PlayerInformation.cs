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

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 20;

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

    public void GiveInformation() {
        FindObjectOfType<PlayerActionController>().SetUpActions(warriorActions, rogueActions, mageActions, clericActions);
        FindObjectOfType<Player>().SetUpHealth(PlayerHealth);
    }

    public void ResetPlayerActions() {
        Destroy(this.gameObject);
    }
}
