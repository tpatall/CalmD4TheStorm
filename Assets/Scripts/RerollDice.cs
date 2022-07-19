using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RerollDice : MonoBehaviour {
    
    public int amount;
    private int maxAmount;

    public TextMeshProUGUI rerollText;

    private void Start() {
        maxAmount = FindObjectOfType<PlayerInformation>().MaxRerolls;
        amount = maxAmount;

        UpdateText();
    }

    public void RestoreDice() {
        amount = maxAmount;
    }

    public bool SpendDice() {
        if(amount > 0) {
            amount--;
            UpdateText();
            return true;
        } else {
            // Corruption
            return false;
        }
    }

    void UpdateText() {
        rerollText.text = amount + "/" + maxAmount;
    }
}
