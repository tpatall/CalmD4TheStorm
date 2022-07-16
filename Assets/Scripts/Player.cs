using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player Instance;

    int health;
    public int maxHealth;
    public int strength;

    public Slider healthBar;

    public enum CharacterType {
        BLANK,
        WARRIOR,
        ROGUE,
        MAGE,
        CLERIC
    }

    public CharacterType currType;

    private void Start() {
        Instance = this;
        health = maxHealth;
    }

    public void SwapCharacter() {
        if(currType == CharacterType.BLANK) {
            SwapCharacterTo(Random.Range(1, 5));
            return;
        }
        int randomIndex = Random.Range(1, 4);
        if(randomIndex >= (int)currType) {
            randomIndex++;
        }

        SwapCharacterTo(randomIndex);
    }

    void SwapCharacterTo(int index) {
        currType = (CharacterType)index;

        FindObjectOfType<PlayerActionController>().UpdateButtons();

        Debug.Log("Player is now of type - " + currType.ToString());
    }

    public void SpendReroll() {
        GetComponent<RerollDice>().SpendDice();
    }

    public void TakeDamage(int damage) {
        if(damage < 0) {
            return;
        }

        health -= damage;

        if(health <= 0) {
            // Die.
            Debug.Log("You have died!");
            transform.GetChild(1).transform.rotation = Quaternion.Euler(0,0,90) ;
        }

        healthBar.value = (float)health / maxHealth;
    }
}
