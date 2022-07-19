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

    private float enemyActionTime = 2f;
    private float playerVictoryTime = 2f;
    private float playerDeathTime = 5f;

    private void Start() {
        player = FindObjectOfType<Player>();
        energy = FindObjectOfType<Energy>();
        enemyController = FindObjectOfType<EnemyController>();

        List<GameObject> enemies = new List<GameObject>();

        EnemyPool[] pools = new EnemyPool[0];

        int level = GameManager.Instance.CurrentLevelIndex;
        if(level < 3) {
            pools = GameManager.Instance.transform.GetChild(0).GetComponents<EnemyPool>();
            enemies = pools[Random.Range(0, pools.Length)].enemyPool;

        } else if (level < 5) {
            pools = GameManager.Instance.transform.GetChild(1).GetComponents<EnemyPool>();
            enemies = pools[Random.Range(0, pools.Length)].enemyPool;

        } else {
            pools = GameManager.Instance.transform.GetChild(2).GetComponents<EnemyPool>();
            enemies = pools[Random.Range(0, pools.Length)].enemyPool;

        }

        StartFight(enemies);
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

    void TurnStart() {
        foreach (Enemy enemy in enemyController.currEnemies) {
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
        foreach (Enemy enemy in enemyController.currEnemies) {
            if (currState == BattleState.DEATH) {
                yield break;
            }
            enemy.PerformAction();
            yield return new WaitForSeconds(enemyActionTime);
        }

        if (currState == BattleState.DEATH) {
            yield break;
        }
        NextState();
    }

    /// <summary>
    ///     On player win.
    /// </summary>
    public void VictoryState() {
        FindObjectOfType<PlayerInformation>().PlayerHealth = player.health;

        StartCoroutine(WaitToDie());
        IEnumerator WaitToDie() {
            playerUI.SetActive(false);

            yield return new WaitForSeconds(playerVictoryTime);

            GameManager.Instance.LeaveBattle();
        }
    }

    /// <summary>
    ///     On player death.
    /// </summary>
    public void DeathState() {
        currState = BattleState.DEATH;

        foreach(Enemy enemy in EnemyController.Instance.currEnemies) {
            enemy.HidePreview();
        }

        StartCoroutine(WaitToDie());
        IEnumerator WaitToDie() {
            playerUI.SetActive(false);

            yield return new WaitForSeconds(playerDeathTime);

            GameManager.Instance.GameOver();
        }
    }
}
