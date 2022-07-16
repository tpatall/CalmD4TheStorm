using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

    public int currEnergy;
    public int maxEnergy;

    public Text energyText;

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
