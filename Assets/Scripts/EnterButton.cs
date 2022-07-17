using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterButton : MonoBehaviour
{
    Overworld overworld;

    // Start is called before the first frame update
    void Start()
    {
        overworld = Overworld.Instance;
    }

    public void EnterLevel() {
        overworld.LoadNextLevel();
    }
}
