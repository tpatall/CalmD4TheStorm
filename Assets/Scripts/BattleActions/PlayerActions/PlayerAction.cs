using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType {
    SELF,
    SINGLE,
    ALL
}

public enum ActionIcon {
    ATTACK,
    BLOCK,
    POISON,
    DEBUFF
}

public interface PlayerAction {
    public int EnergyCost { get; set; }

    public TargetType Target { get; set; }

    public string ActionText { get; set; }

    public bool SkipReroll { get; set; }

    public int[] PrepareAction();

    public void DoAction(Enemy[] targets, int[] numbersRolled);
}
