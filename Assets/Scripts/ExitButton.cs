using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    GameManager overworld;

    // Start is called before the first frame update
    void Start() {
        overworld = GameManager.Instance;
    }

    public void ExitLevel() {
        overworld.LoadNextLevel();
    }
}
