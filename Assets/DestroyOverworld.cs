using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverworld : MonoBehaviour
{
    public void ResetSavedSettings() {
        Overworld.Instance.RegenerateMap();
        FindObjectOfType<PlayerInformation>().ResetPlayerActions();
    }
}
