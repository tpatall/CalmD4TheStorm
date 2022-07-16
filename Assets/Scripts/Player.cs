using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player Instance;

    int health;
    public int maxHealth;

    public int block;
    public int strength;

    public Slider healthBar;

    public GameObject blockIcon;
    public Text blockText;

    public Animator anim;

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

        switch((CharacterType)index) {
            case CharacterType.WARRIOR:
                anim.SetTrigger("warriorTrigger");
                break;
            case CharacterType.ROGUE:
                anim.SetTrigger("rogueTrigger");
                break;
            case CharacterType.MAGE:
                anim.SetTrigger("mageTrigger");
                break;
            case CharacterType.CLERIC:
                anim.SetTrigger("clericTrigger");
                break;
        }

        FindObjectOfType<PlayerActionController>().UpdateButtons();

        Debug.Log("Player is now of type - " + currType.ToString());
    }

    public void SpendReroll() {
        GetComponent<RerollDice>().SpendDice();
    }

    public void TakeDamage(int damage) {
        Debug.Log("Player took " + damage + " damage.");
        if(damage < 0) {
            return;
        }

        if(block >= damage) {
            block -= damage;
        } else {
            damage -= block;
            block = 0;
            health -= damage;
        }

        UpdateBlock();

        if(health <= 0) {
            // Die.
            Debug.Log("You have died!");
            transform.GetChild(1).transform.rotation = Quaternion.Euler(0,0,90) ;
        }

        healthBar.value = (float)health / maxHealth;
    }

    public void Heal(int heal) {
        if(health + heal >= maxHealth) {
            health = maxHealth;
        } else {
            health += heal;
        }

        healthBar.value = (float)health / maxHealth;
    }

    public void GainBlock(int block) {
        this.block += block;
        UpdateBlock();
    }

    public void RemoveBlock() {
        block = 0;
        UpdateBlock();
    }

    void UpdateBlock() {
        if(block == 0) {
            blockIcon.SetActive(false);
        } else {
            blockIcon.SetActive(true);
            blockText.text = block.ToString();
        }
    }
}
