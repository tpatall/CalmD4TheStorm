using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

    int health;
    public int strength;

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
}
