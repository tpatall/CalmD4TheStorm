using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionController : MonoBehaviour {
    List<PlayerAction> warriorActions = new List<PlayerAction>();
    List<PlayerAction> rogueActions = new List<PlayerAction>();
    List<PlayerAction> mageActions = new List<PlayerAction>();
    List<PlayerAction> clericActions = new List<PlayerAction>();

    public Text[] actionTexts, actionCosts;

    private void Start() {
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

    PlayerAction readiedAction;
    
    public void UpdateButtons() {
        for(int i = 0; i < actionTexts.Length; i++) {
            PlayerAction action = GetActionFromIndex(i);

            actionTexts[i].text = action.ActionText;
            actionCosts[i].text = action.EnergyCost.ToString();
        }
    }

    public void ActivateAction(int index) {
        readiedAction = GetActionFromIndex(index);

        int[] numbersRolled = readiedAction.PrepareAction();

        ActionPreviewController.Instance.ShowPreview(numbersRolled, Player.Instance.strength);
    }

    public PlayerAction GetActionFromIndex(int index) {
        switch(Player.Instance.currType) {
            case Player.CharacterType.BLANK:
                Debug.LogError("Blank character has no actions.");
                break;
            case Player.CharacterType.WARRIOR:
                return warriorActions[index];
            case Player.CharacterType.ROGUE:
                return rogueActions[index];
            case Player.CharacterType.MAGE:
                return mageActions[index];
            case Player.CharacterType.CLERIC:
                return clericActions[index];
        }
        return null;
    }
}
