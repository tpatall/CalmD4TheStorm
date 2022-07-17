using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterButton : MonoBehaviour
{
    public void EnterLevel() {
        SceneManager.LoadScene("BattleScene");
    }
}
