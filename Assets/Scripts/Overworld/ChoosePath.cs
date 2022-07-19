using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePath : MonoBehaviour
{
    GameManager overworld;

    // Start is called before the first frame update
    void Start() {
        overworld = GameManager.Instance;
    }

    public void ChooseLeft(bool left) {
        overworld.ChooseLevelLeft(left);
    }
}
