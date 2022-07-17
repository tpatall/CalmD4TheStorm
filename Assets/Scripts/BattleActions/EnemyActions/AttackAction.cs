using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : MonoBehaviour, BattleAction {

    public Enemy enemy;
    public DiceType diceType;
    public int diceAmount;

    private AudioSource audioSource;
    private AudioClip clip;

    void Start() {
        audioSource = Camera.main.GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Audio/Sound Effects/Impact");
    }

    public void DoAction() {
        DiceType damageDiceType = diceType;

        audioSource.clip = clip;
        audioSource.Play();

        if(enemy.debuffed && damageDiceType > 0) {
            damageDiceType--;
        }

        for(int i = 0; i < diceAmount; i++) {
            int damage = DiceHelper.GetRandomFromDice(damageDiceType) + enemy.Strength - enemy.strengthDebuff;

            Player.Instance.TakeDamage(damage);

            Debug.Log("Hit for " + damage + " damage!");
        }
    }

    public string GetActionIcon() {
        return "attackIcon";
    }

    public string GetActionText() {
        string attackString = "";

        DiceType textDiceType = diceType;

        if(enemy.debuffed && textDiceType > 0) {
            textDiceType--;
        }

        if(diceAmount > 1) {
            attackString += diceAmount;
        }

        if(enemy.Strength - enemy.strengthDebuff != 0 && diceAmount > 1) {
            attackString += "(";
        }

        attackString += textDiceType.ToString();

        if(enemy.Strength - enemy.strengthDebuff > 0) {
            attackString += " + " + (enemy.Strength - enemy.strengthDebuff);
        } else if(enemy.Strength - enemy.strengthDebuff < 0) {
            attackString += " - " + Mathf.Abs(enemy.Strength - enemy.strengthDebuff);
        }

        if(enemy.Strength - enemy.strengthDebuff != 0 && diceAmount > 1) {
            attackString += ")";
        }

        return attackString;
    }
}
