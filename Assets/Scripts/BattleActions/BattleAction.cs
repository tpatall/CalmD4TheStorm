using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleAction {
    
    string GetActionIcon();

    string GetActionText();

    void DoAction();
}
