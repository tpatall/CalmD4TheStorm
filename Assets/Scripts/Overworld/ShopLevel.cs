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

    [SerializeField] private Button button1;
    [SerializeField] private Button button2;

    private PlayerInformation playerInformation;

    private List<(CharacterType, PlayerAction)> shopItems;

    private void Start() {
        playerInformation = FindObjectOfType<PlayerInformation>();
        shopItems = playerInformation.BuildShop(2, true);

        image1.sprite = GetSpriteFromClass(shopItems[0].Item1);
        classType1.text = shopItems[0].Item1.ToString();
        abilityPreview1.text = shopItems[0].Item2.ActionText;
        upgradeText1.text = shopItems[0].Item2.GetUpgradeText();

        image2.sprite = GetSpriteFromClass(shopItems[1].Item1);
        classType2.text = shopItems[1].Item1.ToString();
        abilityPreview2.text = shopItems[1].Item2.ActionText;
        upgradeText2.text = shopItems[1].Item2.GetUpgradeText();

        //for (int i = 0; i < shopItems.Count; i++) {
        //    PlayerAction playerAction = shopItems[i].Item1;
        //    int classType = shopItems[i].Item2;
        //    string upgradeText = shopItems[i].Item3;
        //    if (i == 1) {
        //        if (classType == 0) {
        //            image1.sprite = warriorSprite;
        //            classType1.text = "WARRIOR";
        //            abilityPreview1.text = upgradeText;
        //            upgradeText1.text = "";
        //        } else if (classType == 1) {
        //            image1.sprite = rogueSprite;
        //            classType1.text = "ROGUE";
        //            abilityPreview1.text = upgradeText;
        //            upgradeText1.text = "";
        //        } else if (classType == 2) {
        //            image1.sprite = mageSprite;
        //            classType1.text = "MAGE";
        //            abilityPreview1.text = upgradeText;
        //            upgradeText1.text = "";
        //        } else {
        //            image1.sprite = clericSprite;
        //            classType1.text = "CLERIC";
        //            abilityPreview1.text = upgradeText;
        //            upgradeText1.text = "";
        //        }
        //    } else {
        //        if (classType == 0) {
        //            image2.sprite = warriorSprite;
        //            classType2.text = "WARRIOR";
        //            abilityPreview2.text = upgradeText;
        //            upgradeText2.text = "";
        //        }
        //        else if (classType == 1) {
        //            image2.sprite = rogueSprite;
        //            classType2.text = "ROGUE";
        //            abilityPreview2.text = upgradeText;
        //            upgradeText2.text = "";
        //        }
        //        else if (classType == 2) {
        //            image2.sprite = mageSprite;
        //            classType2.text = "MAGE";
        //            abilityPreview2.text = upgradeText;
        //            upgradeText2.text = "";
        //        }
        //        else {
        //            image2.sprite = clericSprite;
        //            classType2.text = "CLERIC";
        //            abilityPreview2.text = upgradeText;
        //            upgradeText2.text = "";
        //        }
        //    }
        //}
    }

    private Sprite GetSpriteFromClass(CharacterType characterType) {
        Sprite sprite;
        switch (characterType) {
            case CharacterType.WARRIOR:
                sprite = warriorSprite;
                break;
            case CharacterType.ROGUE:
                sprite = rogueSprite;
                break;
            case CharacterType.MAGE:
                sprite = mageSprite;
                break;
            case CharacterType.CLERIC:
                sprite = clericSprite;
                break;
            default:
                sprite = null;
                break;
        }
        return sprite;
    }

    public void UpgradeItem1() {
        shopItems[0].Item2.Upgrade();
        Debug.Log("Item1 upgraded!");
        button1.interactable = false;
    }

    public void UpgradeItem2() {
        shopItems[1].Item2.Upgrade();
        Debug.Log("Item2 upgraded!");
        button2.interactable = false;
    }

    public void LeaveShop() {
        Overworld.Instance.LoadNextLevel();
    }
}