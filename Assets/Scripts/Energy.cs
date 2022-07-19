using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Energy : MonoBehaviour {

    public int currEnergy;
    private int maxEnergy;

    public TextMeshProUGUI energyText;

    private void Start() {
        maxEnergy = FindObjectOfType<PlayerInformation>().MaxEnergy;
        currEnergy = maxEnergy;
    }

    public void SpendEnergy(int energyCost) {
        if(currEnergy >= energyCost) {
            currEnergy -= energyCost;
        }

        energyText.text = currEnergy + "/" + maxEnergy;
    }

    public void RefreshEnergy() {
        currEnergy = maxEnergy;

        energyText.text = currEnergy + "/" + maxEnergy;
    }
}
