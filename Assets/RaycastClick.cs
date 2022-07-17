using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastClick : MonoBehaviour
{
    private LevelObject lastLevelObject;

    void Update() {
        //Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit;
        //if (hit = Physics2D.Raycast(origin, Vector3.back)) {
        //    if (hit.transform != null) {
        //        lastLevelObject = hit.transform.gameObject.GetComponent<LevelObject>();
        //        if (lastLevelObject != null && lastLevelObject.Next) {
        //            lastLevelObject.HighlightSprite(true);
        //        }
        //    }
        //} else if (lastLevelObject != null && lastLevelObject.Next) {
        //    lastLevelObject.HighlightSprite(false);
        //}

        //if (Input.GetMouseButtonDown(0)) {
        //    if (hit = Physics2D.Raycast(origin, Vector3.back)) {
        //        if (hit.transform != null) {
        //            if (lastLevelObject != null) {
        //                //if (lastLevelObject.Next) 
        //                    //Overworld.Instance.GoNextLevel(lastLevelObject.CurrentLevelIndex, lastLevelObject.transform.position);
        //            }
        //        }
        //    }
        //}
    }
}
