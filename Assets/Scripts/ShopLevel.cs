using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopLevel : MonoBehaviour
{
    [SerializeField] Image image1;
    [SerializeField] TextMeshProUGUI classType1;
    [SerializeField] TextMeshProUGUI abilityPreview1;
    [SerializeField] TextMeshProUGUI upgradeText1;

    [SerializeField] Image image2;
    [SerializeField] TextMeshProUGUI classType2;
    [SerializeField] TextMeshProUGUI abilityPreview2;
    [SerializeField] TextMeshProUGUI upgradeText2;

    [SerializeField] private Sprite warriorSprite;
    [SerializeField] private Sprite rogueSprite;
    [SerializeField] private Sprite mageSprite;
    [SerializeField] private Sprite clericSprite;

    private PlayerInformation playerInformation;

    private List<(PlayerAction, int, string)> shopItems;

    private void Start() {
        playerInformation = FindObjectOfType<PlayerInformation>();
        shopItems = playerInformation.BuildShop();

        for (int i = 0; i < shopItems.Count; i++) {
            PlayerAction playerAction = shopItems[i].Item1;
            int classType = shopItems[i].Item2;
            string upgradeText = shopItems[i].Item3;
            if (i == 1) {
                if (classType == 0) {
                    image1.sprite = warriorSprite;
                    classType1.text = "WARRIOR";
                    abilityPreview1.text = upgradeText;
                    upgradeText1.text = "";
                } else if (classType == 1) {
                    image1.sprite = rogueSprite;
                    classType1.text = "ROGUE";
                    abilityPreview1.text = upgradeText;
                    upgradeText1.text = "";
                } else if (classType == 2) {
                    image1.sprite = mageSprite;
                    classType1.text = "MAGE";
                    abilityPreview1.text = upgradeText;
                    upgradeText1.text = "";
                } else {
                    image1.sprite = clericSprite;
                    classType1.text = "CLERIC";
                    abilityPreview1.text = upgradeText;
                    upgradeText1.text = "";
                }
            } else {
                if (classType == 0) {
                    image2.sprite = warriorSprite;
                    classType2.text = "WARRIOR";
                    abilityPreview2.text = upgradeText;
                    upgradeText2.text = "";
                }
                else if (classType == 1) {
                    image2.sprite = rogueSprite;
                    classType2.text = "ROGUE";
                    abilityPreview2.text = upgradeText;
                    upgradeText2.text = "";
                }
                else if (classType == 2) {
                    image2.sprite = mageSprite;
                    classType2.text = "MAGE";
                    abilityPreview2.text = upgradeText;
                    upgradeText2.text = "";
                }
                else {
                    image2.sprite = clericSprite;
                    classType2.text = "CLERIC";
                    abilityPreview2.text = upgradeText;
                    upgradeText2.text = "";
                }
            }
        }
    }

    public void UpgradeItem1() {
        shopItems[0].Item1.Upgrade();
    }

    public void UpgradeItem2() {
        shopItems[1].Item1.Upgrade();
    }

    public void LeaveShop() {
        Overworld.Instance.LoadNextLevel();
    }
}