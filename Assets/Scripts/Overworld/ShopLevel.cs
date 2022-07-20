using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopLevel : MonoBehaviour
{
    public List<UpgradeUI> upgradeUIs = new List<UpgradeUI>();

    [SerializeField] private TextMeshProUGUI currentMoneyText;

    [SerializeField] private Sprite warriorSprite;
    [SerializeField] private Sprite rogueSprite;
    [SerializeField] private Sprite mageSprite;
    [SerializeField] private Sprite clericSprite;

    private PlayerInformation playerInformation;

    private List<(CharacterType, PlayerAction)> shopItems;

    private int currentMoney;

    private void Start() {
        playerInformation = PlayerInformation.Instance;

        currentMoney = playerInformation.CurrentMoney;
        currentMoneyText.text = currentMoney.ToString();
        
        shopItems = playerInformation.BuildShop(4, true);

        for (int i = 0; i < upgradeUIs.Count; i++) {
            if (i >= shopItems.Count) {
                upgradeUIs[i].gameObject.SetActive(false);
            } else {
                BuildItem(upgradeUIs[i], shopItems[i].Item2, shopItems[i].Item1);
            }
        }
    }

    private void BuildItem(UpgradeUI upgradeUI, PlayerAction playerAction, CharacterType characterType) {
        upgradeUI.image.sprite = GetSpriteFromClass(characterType);
        upgradeUI.classType.text = characterType.ToString();
        upgradeUI.abilityPreview.text = playerAction.ActionText;
        upgradeUI.upgradeText.text = playerAction.GetUpgradeText();
        upgradeUI.upgradeCost.text = playerAction.UpgradeCost.ToString();
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

    public void UpgradeItemCheck(UpgradeUI upgradeUI) {
        int upgradeCost = int.Parse(upgradeUI.upgradeCost.text);
        if (upgradeCost > currentMoney) return;

        Debug.Log(upgradeUI.classType.text + " upgraded!");
        upgradeUI.GetComponent<Button>().interactable = false;
        UpgradeRelevantItem(upgradeUI);

        SuccessfulPurchase(upgradeCost);
    }

    private void UpgradeRelevantItem(UpgradeUI upgradeUI) {
        for (int i = 0; i < shopItems.Count; i++) {
            // Upgrade item based on index.
            if (upgradeUIs[i] == upgradeUI) {
                shopItems[i].Item2.Upgrade();
            }
        }
    }

    private void SuccessfulPurchase(int upgradeCost) {
        currentMoney -= upgradeCost;
        currentMoneyText.text = currentMoney.ToString();
        Debug.Log("New current money: " + currentMoney);
    }

    public void LeaveShop() {
        playerInformation.CurrentMoney = currentMoney;
        
        GameManager.Instance.NextLevel();
    }
}