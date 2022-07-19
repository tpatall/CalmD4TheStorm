using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofCaller : MonoBehaviour {
    public Poof poof;
    public void StartPoof() {
        poof.AnimationStart();
    }
}
