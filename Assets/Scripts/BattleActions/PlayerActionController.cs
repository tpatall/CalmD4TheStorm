using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerActionController : MonoBehaviour {
    List<PlayerAction> warriorActions = new List<PlayerAction>();
    List<PlayerAction> rogueActions = new List<PlayerAction>();
    List<PlayerAction> mageActions = new List<PlayerAction>();
    List<PlayerAction> clericActions = new List<PlayerAction>();

    public TextMeshProUGUI[] actionTexts, actionCosts;

    public GameObject targetAll, targetSelf;

    int[] numbersRolled;
    Enemy[] targets;

    public void SetUpActions(
        List<PlayerAction> warriorActions, 
        List<PlayerAction> rogueActions, 
        List<PlayerAction> mageActions, 
        List<PlayerAction> clericActions) {
        this.warriorActions = warriorActions;
        this.rogueActions = rogueActions;
        this.mageActions = mageActions;
        this.clericActions = clericActions;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(1)) {
            RemoveActivation();
        }
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
        RemoveActivation();

        if(numbersRolled != null) {
            DoAction();
        }

        readiedAction = GetActionFromIndex(index);

        if(readiedAction.EnergyCost > FindObjectOfType<Energy>().currEnergy) {
            return;
        }

        switch(readiedAction.Target) {
            case TargetType.SELF:
                targetSelf.SetActive(true);
                break;
            case TargetType.SINGLE:
                foreach(Enemy enemy in EnemyController.Instance.currEnemies) {
                    enemy.targetObject.SetActive(true);
                }
                break;
            case TargetType.ALL:
                targetAll.SetActive(true);
                break;
        }
    }

    public void PreviewAction(GameObject gameObject) {
        RemoveActivation();

        numbersRolled = readiedAction.PrepareAction();


        switch(readiedAction.Target) {
            case TargetType.SINGLE:
                Enemy enemy = gameObject.GetComponent<Enemy>();
                targets = new Enemy[1] { enemy };
                break;
            case TargetType.ALL:
                targets = EnemyController.Instance.currEnemies.ToArray();
                break;
            default:
                targets = null;
                break;
        }

        FindObjectOfType<Energy>().SpendEnergy(readiedAction.EnergyCost);

        if(!readiedAction.SkipReroll) {
            if(readiedAction.Target == TargetType.SELF) {
                ActionPreviewController.Instance.ShowPreview(numbersRolled, 0);
            } else {
                ActionPreviewController.Instance.ShowPreview(numbersRolled, Player.Instance.strength);
            }
        } else {
            DoAction();
        }
    }

    public void DoAction() {
        if(readiedAction != null && numbersRolled != null) {
            readiedAction.DoAction(targets, numbersRolled);
        }

        readiedAction = null;
        numbersRolled = null;
        targets = null;

        ActionPreviewController.Instance.HidePreview();
    }

    public void RerollAction() {
        if(FindObjectOfType<RerollDice>().SpendDice()) {
            numbersRolled = readiedAction.PrepareAction();
            ActionPreviewController.Instance.ShowPreview(numbersRolled, Player.Instance.strength);
        }
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

    public void RemoveActivation() {
        foreach(Enemy target in EnemyController.Instance.currEnemies) {
            target.targetObject.SetActive(false);
        }

        targetAll.SetActive(false);
        targetSelf.SetActive(false);
    }
}
