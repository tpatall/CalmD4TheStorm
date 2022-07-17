using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    Energy energy;

    EnemyController enemyController;

    public GameObject playerUI;


    private void Start() {
        player = FindObjectOfType<Player>();
        energy = FindObjectOfType<Energy>();
        enemyController = FindObjectOfType<EnemyController>();
        StartFight(null);
    }

    public void VictoryState() {
        FindObjectOfType<PlayerInformation>().PlayerHealth = player.health;

        Overworld.Instance.LoadNextLevel();
    }

    public void StartFight(List<GameObject> enemyObjects) {
        currState = BattleState.INITIATE;

        // Create Enemies
        enemyController.SpawnEnemies(enemyObjects);

        // Optional cinematic effects


        // Apply "Start of Battle" effects


        NextState();
    }

    public void NextState() {
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

    public void DeathState() {
        currState = BattleState.DEATH;

        foreach(Enemy enemy in EnemyController.Instance.currEnemies) {
            enemy.HidePreview();
        }

        StartCoroutine(WaitToDie());

        IEnumerator WaitToDie() {
            yield return new WaitForSeconds(6f);

            SceneManager.LoadScene("GameOver");
        }

        // Go to death screen
    }

    void TurnStart() {
        foreach(Enemy enemy in enemyController.currEnemies) {
            enemy.debuffed = false;
            enemy.strengthDebuff = 0;
            enemy.ReadyRandomAction();
        }

        player.SwapCharacter();

        player.RemoveBlock();

        ActionPreviewController.Instance.HidePreview();

        playerUI.SetActive(true);

        energy.RefreshEnergy();
    }

    IEnumerator EnemyTurn() {
        foreach(Enemy enemy in enemyController.currEnemies) {
            if(currState == BattleState.DEATH) {
                yield break;
            }
            enemy.PerformAction();
            yield return new WaitForSeconds(2);
        }

        if(currState == BattleState.DEATH) {
            yield break;
        }
        NextState();
    }
}
