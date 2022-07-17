using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public int strengthDebuff;

    private int dexterity;
    public int Dexterity {
        get {
            return dexterity;
        }
        set {
            dexterity = value;
        }
    }

    public bool debuffed;

    public TextMeshProUGUI healthText;
    public Slider healthBar;

    public GameObject previewObject;
    public TextMeshProUGUI previewText;
    public Image previewImage;

    public GameObject targetObject;

    BattleAction[] battleActions;
    public BattleAction readiedAction;

    public void Initialize() {
        // Read actions.
        battleActions = GetComponents<BattleAction>();

        health = maxHealth;
        UpdateHealthBar();
    }

    public void ReadyRandomAction() {
        readiedAction = battleActions[Random.Range(0, battleActions.Length)];

        previewObject.SetActive(true);

        previewText.text = readiedAction.GetActionText();
        previewImage.sprite = Resources.Load<Sprite>(readiedAction.GetActionIcon());

        Debug.Log("Readied the " + readiedAction.ToString() + " action.");
    }

    public void PerformAction() {
        if(readiedAction == null) {
            Debug.LogError("No readied action!");
            return;
        }

        previewObject.SetActive(false);
        readiedAction.DoAction();
    }

    public bool ApplyDamage(int damage) {
        health -= damage;

        if(health <= 0) {
            // Die.
            EnemyController.Instance.Kill(this);
            return true;
        }

        UpdateHealthBar();

        return false;
    }

    private void UpdateHealthBar() {
        healthBar.value = (float)health / maxHealth;

        healthText.text = health + "/" + maxHealth; 
    }

    public void Targeted() {
        FindObjectOfType<PlayerActionController>().PreviewAction(gameObject);
    }
}
