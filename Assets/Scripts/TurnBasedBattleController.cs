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

    [SerializeField]
    List<ActionButton> actionButtons = new List<ActionButton>();

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

        GameManager gameManager = GameManager.Instance;
        List<GameObject> enemies = gameManager.LevelObjects[gameManager.CurrentLevelIndex - 1].Enemies;

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
        UpdateActionButtons();
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

    private void UpdateActionButtons() {
        for (int i = 0; i < actionButtons.Count; i++) {
            actionButtons[i].UpdateValues();
        }
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

            // Get paid
            GameManager gameManager = GameManager.Instance;
            int reward = gameManager.LevelObjects[gameManager.CurrentLevelIndex - 1].Reward;
            PlayerInformation.Instance.CurrentMoney += reward;

            gameManager.NextLevel();
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
