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

    [SerializeReference] List<BattleAction> battleActions;
}
