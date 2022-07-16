using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAction : MonoBehaviour, BattleAction {
    Enemy enemy;
    public int amount;

    void Start() {
        enemy = GetComponent<Enemy>();
    }

    public void DoAction() {
        enemy.Strength += amount;
        Debug.Log("Buffing strength!");
    }

    public string GetActionIcon() {
        return "buffIcon";
    }

    public string GetActionText() {
        return amount.ToString();
    }
}
