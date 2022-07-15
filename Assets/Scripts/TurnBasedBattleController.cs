using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedBattleController : MonoBehaviour {

    enum BattleState { 
        INITIATE,
        PLAYER_TURN,
        ENEMY_TURN,
        VICTORY,
        DEATH
    }

    BattleState currState;

    public void StartFight() {
        currState = BattleState.INITIATE;

        // Create Enemies

        // Optional cinematic effects

        // Apply "Start of Battle" effects

        NextState();
    }

    void NextState() {
        switch(currState) {
            case BattleState.INITIATE:
                currState = BattleState.PLAYER_TURN;
                break;
            case BattleState.PLAYER_TURN:
                currState = BattleState.ENEMY_TURN;
                break;
            case BattleState.ENEMY_TURN:
                currState = BattleState.PLAYER_TURN;
                break;
            default:
                Debug.LogError("NextState shouldn't be called in the current state.");
                break;
        }
    }
}
