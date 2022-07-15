using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour {
    public string name;

    public int maxHealth;

    private int health;

    private int strength;
    public int Strength {
        get {
            return strength;
        }
        set {
            strength = value;
        }
    }

    private int dexterity;
    public int Dexterity {
        get {
            return dexterity;
        }
        set {
            dexterity = value;
        }
    }

    BattleAction[] battleActions;
    BattleAction readiedAction;

    public void Initialize() {
        // Read actions.
        battleActions = GetComponents<BattleAction>();
    }

    public void ReadyRandomAction() {
        readiedAction = battleActions[Random.Range(0, battleActions.Length)];
        Debug.Log("Readied the " + readiedAction.ToString() + " action.");
    }

    public void PerformAction() {
        if(readiedAction == null) {
            Debug.LogError("No readied action!");
            return;
        }
        readiedAction.DoAction();
    }
}
