using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld : PersistentSingleton<Overworld>
{
    /// <summary>
    ///     First next level for testing purposes.
    /// </summary>
    public int StartLevel = 1;

    /// <summary>
    ///     Position of the starting position.
    /// </summary>
    public Vector3 PlayerPosition;

    [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private GameObject battleLevelPrefab;
    [SerializeField] private GameObject shopLevelPrefab;
    [SerializeField] private GameObject treasureLevelPrefab;

    [SerializeField] private List<LevelTypes> Levels = new List<LevelTypes>();

    /// <summary>
    ///     List of level objects in scene.
    ///     Used to get information about next level.
    /// </summary>
    private List<LevelObject> levelObjects = new List<LevelObject>();

    private int currentLevelIndex = 0;

    // Levels since the start.
    private int progression = 0;

    public GameState CurrentState { get; private set; }

    void Start()
    {
        PopulateMap();
        CurrentState = GameState.WaitForEnter;
    }

    private void PopulateMap() {
        GenerateMap generateMap = new GenerateMap(Levels.Count);
        List<Branch> branches = generateMap.Branches;

        int levelID;
        // Build the map branch by branch (to avoid crossing paths).
        for (int i = 0; i < branches.Count; i++) {
            for (int j = 0; j < branches[i].Levels.Count; j++) {
                Level level = branches[i].Levels[j];

                // Choose the level type from the specified level types based on progression in the game.
                // With a chance to divert?
                levelID = level.LevelID;

                Vector2 gameObjectPos = new Vector2(levelID * 3f, 0.5f - (2 * i));

                switch (Levels[levelID]) {
                    case LevelTypes.BattleLevel:
                        InstantiateLevel(battleLevelPrefab, level, gameObjectPos);
                        break;
                    case LevelTypes.ShopLevel:
                        InstantiateLevel(shopLevelPrefab, level, gameObjectPos);
                        break;
                    case LevelTypes.TreasureLevel:
                        InstantiateLevel(treasureLevelPrefab, level, gameObjectPos);
                        break;
                    default:
                        break;
                }

                currentLevelIndex++;
            }
        }

        // Enable the new possible to-be selected levels to be interacted with.
        LevelObject levelObject = levelObjects[0];
        foreach (Level level in levelObject.Level.NextLevels) {
            level.Object.Next = true;
            Debug.Log("Level " + level.Object.CurrentLevelIndex + " is enabled.");
        }
    }

    /// <summary>
    ///     Prevent repetitive code between switch cases.
    /// </summary>
    /// <param name="prefab">Prefab to instantiate.</param>
    /// <param name="level">Level that belongs to this object.</param>
    /// <param name="pos">Position for this object.</param>
    private void InstantiateLevel(GameObject prefab, Level level, Vector2 pos) {
        GameObject gameObject = Instantiate(prefab, transform);
        gameObject.transform.position = pos;

        LevelObject levelObject = gameObject.GetComponent<LevelObject>();
        levelObject.SetUp(currentLevelIndex, level, this);
        level.AssignLevelObject(levelObject);

        levelObjects.Add(levelObject);
    }

    /// <summary>
    ///     Get the level type of the next level and randomly generate the content.
    /// </summary>
    public void StartNextLevel() {
        CurrentState = GameState.InLevel;
        Debug.Log("Entering level: " + Levels[progression]);

        switch (Levels[progression]) {
            case LevelTypes.BattleLevel:
                GenerateBattleLevel();
                break;
            case LevelTypes.ShopLevel:
                GenerateShopLevel();
                break;
            case LevelTypes.TreasureLevel:
                GenerateTreasureLevel();
                break;
            default:
                Debug.Log("This aint no level fool");
                break;
        }
    }

    /// <summary>
    ///     Start battle level.
    /// </summary>
    private void GenerateBattleLevel() {
        // play battle animation

        // proceed to encounter class

        CurrentState = GameState.WaitForMoveNext;
        Debug.Log("Waiting for movement.");
    }


    /// <summary>
    ///     Start shop level.
    /// </summary>
    private void GenerateShopLevel() {
        // play shop animation

        // proceed to shop class

        CurrentState = GameState.WaitForMoveNext;
        Debug.Log("Waiting for movement.");
    }

    /// <summary>
    ///     Start treasure level.
    /// </summary>
    private void GenerateTreasureLevel() {
        // play treasure animation

        // proceed to treasure class

        CurrentState = GameState.WaitForMoveNext;
        Debug.Log("Waiting for movement.");
    }

    /// <summary>
    ///     Move to next level.
    /// </summary>
    /// <returns>Position of the next level object.</returns>
    public void GoNextLevel(int currentLevelIndex, Vector2 objectPos) {
         CurrentState = GameState.Walking;
        Debug.Log("Walking to next position.");

        // Disable the possible selected levels from being interacted with now that one is chosen.
        //LevelObject levelObject = levelObjects[currentLevelIndex];
        //foreach (Level level in levelObject.Level.NextLevels) {
        //    level.Object.Next = false;
        //}

        this.currentLevelIndex = currentLevelIndex;

        // Enable the new possible to-be selected levels to be interacted with.
        LevelObject levelObject = levelObjects[currentLevelIndex];
        foreach (Level level in levelObject.Level.NextLevels) {
            level.Object.Next = true;
            Debug.Log("Level " + level.Object.CurrentLevelIndex + " is enabled.");
        }

        progression++;
        playerMovement.MoveToNext(objectPos);

        CurrentState = GameState.WaitForEnter;
        Debug.Log("Waiting to enter level.");
    }
}

enum LevelTypes
{
    BattleLevel,
    ShopLevel,
    TreasureLevel
}

public enum GameState
{
    WaitForMoveNext,
    Walking,
    WaitForEnter,
    InLevel
}

