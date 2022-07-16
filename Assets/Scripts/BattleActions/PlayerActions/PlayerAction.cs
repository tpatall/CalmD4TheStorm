using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerAction {
    public int EnergyCost { get; set; }

    public bool RequiresTarget { get; set; }

    public string ActionText { get; set; }

    public int[] PrepareAction();

    public void DoAction(Enemy[] targets, int[] numbersRolled);
}
