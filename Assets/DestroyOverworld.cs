using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverworld : MonoBehaviour
{
    public void RegenerateOverworld() {
        Overworld.Instance.RegenerateMap();
    }
}
