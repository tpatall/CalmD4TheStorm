using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poof : MonoBehaviour {
    Animator anim;

    public void AnimationStart() {
        anim = GetComponent<Animator>();
        gameObject.SetActive(true);
        anim.Play("Poof");
    }

    void AnimationEnd() {
        gameObject.SetActive(false);
    }
}
