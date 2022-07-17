using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RerollDice : MonoBehaviour {
    public int amount;

    [SerializeField] int maxAmount;

    public TextMeshProUGUI rerollText;

    private void Start() {
        amount = maxAmount;
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
