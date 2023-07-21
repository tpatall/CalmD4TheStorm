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

    private void Start() {
        PlayerInformation playerInformation = FindObjectOfType<PlayerInformation>();

        this.warriorActions = playerInformation.warriorActions;
        this.rogueActions = playerInformation.rogueActions;
        this.mageActions = playerInformation.mageActions;
        this.clericActions = playerInformation.clericActions;
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

        switch (readiedAction.Target) {
            case TargetType.SELF:
                targetSelf.SetActive(true);
                break;
            case TargetType.SINGLE:
                bool debuffDiceType = false;
                if (readiedAction.ActionText == "REDUCE DICE VALUE") {
                    debuffDiceType = true;
                }

                foreach(Enemy enemy in EnemyController.Instance.currEnemies) {
                    if (debuffDiceType) {
                        string enemyActionText = enemy.readiedAction.GetActionText();
                        bool lowestDiceType = enemyActionText.Contains("D4");
                        // If DiceType is D4, it cannot be reduced further.
                        if (lowestDiceType) {
                            continue;
                        }
                    }

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

        if (!readiedAction.SkipReroll) {
            if(readiedAction.Target == TargetType.SELF) {
                ActionPreviewController.Instance.ShowPreview(numbersRolled, 0);
            } else {
                ActionPreviewController.Instance.ShowPreview(numbersRolled, Player.Instance.Strength);
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
            ActionPreviewController.Instance.ShowPreview(numbersRolled, Player.Instance.Strength);
        }
    }

    public PlayerAction GetActionFromIndex(int index) {
        switch(Player.Instance.CurrentCharacterType) {
            case CharacterType.BLANK:
                Debug.LogError("Blank character has no actions.");
                break;
            case CharacterType.WARRIOR:
                return warriorActions[index];
            case CharacterType.ROGUE:
                return rogueActions[index];
            case CharacterType.MAGE:
                return mageActions[index];
            case CharacterType.CLERIC:
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
