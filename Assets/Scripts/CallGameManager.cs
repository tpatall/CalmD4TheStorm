using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGameManager : MonoBehaviour
{
    public void EnterBattle() => GameManager.Instance.EnterBattle();

    public void ResetWorld() => GameManager.Instance.ResetWorld();
}
