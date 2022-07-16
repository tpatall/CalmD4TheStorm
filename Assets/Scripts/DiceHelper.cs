using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceHelper {
    public static int GetRandomFromDice(DiceType diceType) {
        int maxValue;

        switch(diceType) {
            case DiceType.D4:
                maxValue = 4;
                break;
            case DiceType.D6:
                maxValue = 6;
                break;
            case DiceType.D8:
                maxValue = 8;
                break;
            case DiceType.D10:
                maxValue = 10;
                break;
            case DiceType.D12:
                maxValue = 12;
                break;
            case DiceType.D20:
                maxValue = 20;
                break;
            default:
                Debug.LogError("Unknown dicetype");
                maxValue = 0;
                break;
        }

        return Random.Range(1, maxValue + 1);
    }
}
