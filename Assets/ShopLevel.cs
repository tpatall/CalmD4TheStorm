using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevel : MonoBehaviour
{
    // 3 deals
    [SerializeField] private List<Deal> items = new List<Deal>();

    private void Start() {

    }

    public void LeaveShop() {
        Overworld.Instance.LoadNextLevel();
    }
}