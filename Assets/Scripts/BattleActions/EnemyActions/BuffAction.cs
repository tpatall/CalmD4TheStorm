using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAction : MonoBehaviour, BattleAction {
    Enemy enemy;
    public int amount;

    private AudioSource audioSource;
    private AudioClip clip;

    void Start() {
        audioSource = Camera.main.GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("Audio/Sound Effects/Buff");

        enemy = GetComponent<Enemy>();
    }

    public void DoAction() {
        audioSource.clip = clip;
        audioSource.Play();

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
