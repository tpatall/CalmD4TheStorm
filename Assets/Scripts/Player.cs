using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {

    public static Player Instance;

    public int health;
    public int maxHealth;

    public int block;

    public Slider healthBar;
    public TextMeshProUGUI healthText;

    public GameObject blockIcon;
    public TextMeshProUGUI blockText;

    public GameObject buffIcon;
    private TextMeshProUGUI buffText;

    public Animator anim;

    public Poof poof;

    public GameObject damagePrefab;

    public int Strength { get; set; }

    public CharacterType CurrentCharacterType { get; set; }

    private void Start() {
        Instance = this;

        health = FindObjectOfType<PlayerInformation>().PlayerHealth;
        healthBar.value = (float)health / maxHealth;
        healthBar.enabled = false;

        buffText = buffIcon.GetComponentInChildren<TextMeshProUGUI>();

        UpdateHealth();
    }

    public void SwapCharacter() {
        poof.AnimationStart();

        if(CurrentCharacterType == CharacterType.BLANK) {
            SwapCharacterTo(Random.Range(1, 5));
            return;
        }
        int randomIndex = Random.Range(1, 4);
        if(randomIndex >= (int)CurrentCharacterType) {
            randomIndex++;
        }

        SwapCharacterTo(randomIndex);
    }

    void SwapCharacterTo(int index) {
        CurrentCharacterType = (CharacterType)index;

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

        Debug.Log("Player is now of type - " + CurrentCharacterType.ToString());
    }

    public void SpendReroll() {
        if(GetComponent<RerollDice>().SpendDice()) {
            SwapCharacter();
        }
    }

    public bool TakeDamage(int damage) {
        Debug.Log("Player took " + damage + " damage.");

        Instantiate(damagePrefab, transform.position, Quaternion.identity).GetComponent<DamageIndicator>().StartCoroutine("MoveAndDisappear", damage);

        if(damage < 0) {
            return false;
        }

        if(block >= damage) {
            block -= damage;
        } else {
            damage -= block;
            block = 0;
            health -= damage;
        }

        UpdateBlock();
        UpdateHealth();

        if(health <= 0) {
            // Die.
            Debug.Log("You have died!");
            anim.SetTrigger("deathTrigger");
            FindObjectOfType<TurnBasedBattleController>().DeathState();
            healthBar.value = (float)health / maxHealth;
            return true;
        }

        healthBar.value = (float)health / maxHealth;
        return false;
    }

    public void Heal(int[] heal) {
        int gainedHealth = 0;
        for (int i = 0; i < heal.Length; i++) gainedHealth += heal[i];

        if (health + gainedHealth >= maxHealth) {
            health = maxHealth;
        } else {
            health += gainedHealth;
        }

        healthBar.value = (float)health / maxHealth;
        healthBar.value = (float)health / maxHealth;

        UpdateHealth();
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

    public void UpdateHealth() {
        // Although the UI is disabled before this is displayed, whynot.
        if (health <= 0) {
            health = 0;
        }

        healthText.text = health + "/" + maxHealth;
    }

    // Buff strength by 1 for the rest of the encounter.
    public void UpdateStrength() {
        Strength++;
        buffText.text = Strength.ToString();
        buffIcon.SetActive(true);
    }
}
