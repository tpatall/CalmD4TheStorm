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

    Player player;

    EnemyController enemyController;

    public GameObject playerUI;


    private void Start() {
        player = FindObjectOfType<Player>();
        enemyController = FindObjectOfType<EnemyController>();
        StartFight(null);
    }

    private void Update() {
        switch(currState) {
            case BattleState.PLAYER_TURN:
                if(Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("Player passed his turn.");
                    NextState();
                }
                break;
            case BattleState.ENEMY_TURN:
                break;
            case BattleState.VICTORY:
                break;
            case BattleState.DEATH:
                break;
            default:
                break;
        }
    }

    public void StartFight(List<GameObject> enemyObjects) {
        currState = BattleState.INITIATE;

        // Create Enemies
        enemyController.SpawnEnemies(enemyObjects);

        // Optional cinematic effects


        // Apply "Start of Battle" effects


        NextState();
    }

    void NextState() {
        switch(currState) {
            case BattleState.INITIATE:
                currState = BattleState.PLAYER_TURN;
                TurnStart();
                break;
            case BattleState.PLAYER_TURN:
                currState = BattleState.ENEMY_TURN;
                playerUI.SetActive(false);
                StartCoroutine(EnemyTurn());
                break;
            case BattleState.ENEMY_TURN:
                currState = BattleState.PLAYER_TURN;
                TurnStart();
                break;
            default:
                Debug.LogError("NextState shouldn't be called in the current state.");
                break;
        }
    }

    void TurnStart() {
        foreach(Enemy enemy in enemyController.currEnemies) {
            enemy.ReadyRandomAction();
        }

        player.SwapCharacter();
        playerUI.SetActive(true);
    }

    IEnumerator EnemyTurn() {
        foreach(Enemy enemy in enemyController.currEnemies) {
            enemy.PerformAction();
            yield return new WaitForSeconds(2);
        }

        NextState();
    }
}
