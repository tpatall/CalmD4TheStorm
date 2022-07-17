using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevel : MonoBehaviour
{
    private PlayerInformation playerInformation;

    private List<PlayerAction> shopItems;

    private void Start() {
        playerInformation = FindObjectOfType<PlayerInformation>();
        shopItems = playerInformation.BuildShop();
    }

    public void LeaveShop() {
        Overworld.Instance.LoadNextLevel();
    }
}