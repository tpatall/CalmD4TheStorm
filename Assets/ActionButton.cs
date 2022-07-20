using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite disabledSprite;

    [SerializeField] private Energy playerEnergy;
    [SerializeField] private TextMeshProUGUI energyCostText;

    private Image image;
    private Button button;

    private int energyCost;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        UpdateValues();
    }

    public void UpdateValues() {
        // Safely assume its always an integer.
        energyCost = int.Parse(energyCostText.text);

        Switch(playerEnergy.currEnergy < energyCost);
    }

    private void Switch(bool costTooHigh) {
        if (costTooHigh) {
            image.sprite = disabledSprite;
            image.color = new Color(1f, 0f, 0f);
            button.interactable = false;
        }
        else {
            image.sprite = enabledSprite;
            image.color = new Color(1f, 1f, 1f);
            button.interactable = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (playerEnergy.currEnergy < energyCost) {
            Switch(true);
        }
    }
}
