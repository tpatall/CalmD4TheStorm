using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendAction : MonoBehaviour, BattleAction {

    Enemy enemy;
    public DiceType diceType;
    public int diceAmount;

    private AudioSource audioSource;
    private AudioClip clip;

    void Start() {
        audioSource = Camera.main.GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Audio/Sound Effects/Block");
    }

    public void DoAction() {
        Debug.Log("Blocking");

        audioSource.clip = clip;
        audioSource.Play();
    }

    public string GetActionIcon() {
        throw new System.NotImplementedException();
    }

    public string GetActionText() {
        string defendString = "";

        if(diceAmount > 0) {
            defendString += diceAmount + "(";
        }

        defendString += diceType.ToString();

        if(enemy.Dexterity > 0) {
            defendString += " + " + enemy.Strength;
        } else if(enemy.Dexterity < 0) {
            defendString += " - " + enemy.Strength;
        }

        if(diceAmount > 0) {
            defendString += diceAmount + ")";
        }

        return defendString;
    }
}
