using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject battleLevelPrefab;
    [SerializeField] private GameObject shopLevelPrefab;
    [SerializeField] private GameObject treasureLevelPrefab;

    [SerializeField] private List<LevelTypes> Levels = new List<LevelTypes>();

    /// <summary>
    ///     List of level objects in scene.
    ///     Used to get information about next level.
    /// </summary>
    private List<LevelObject> levelObjects = new List<LevelObject>();

    // Current index in levelObjects list.
    private int currentLevelIndex = 0;

    // List of possible levels to go to next.
    private List<LevelObject> nextObjects = new List<LevelObject>();

    void Start()
    {
        PopulateMap();

        LoadNextLevel();
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
                LevelTypes levelType = Levels[levelID];
                // If a battle or treasure level is between 3 and 3-before final boss, then allow variety based on random chance.
                if (levelID > 3 && levelID < Levels.Count - 3
                    && Levels[levelID - 1] != Levels[levelID] && Levels[levelID + 1] != Levels[levelID]) {
                    if (levelType == LevelTypes.BattleLevel) {
                        // 20% chance for a battle level to become a treasure level.
                        if (Random.Range(0f, 1f) < 0.2f) {
                            levelType = LevelTypes.TreasureLevel;
                        }
                    } else if (levelType == LevelTypes.TreasureLevel) {
                        // 40% chance for a treasure level to become a battle level.
                        if (Random.Range(0f, 1f) < 0.4f) {
                            levelType = LevelTypes.BattleLevel;
                        }
                    }
                }

                switch (levelType) {
                    case LevelTypes.BattleLevel:
                        InstantiateLevel(battleLevelPrefab, level, LevelTypes.BattleLevel);
                        break;
                    case LevelTypes.ShopLevel:
                        InstantiateLevel(shopLevelPrefab, level, LevelTypes.ShopLevel);
                        break;
                    case LevelTypes.TreasureLevel:
                        InstantiateLevel(treasureLevelPrefab, level, LevelTypes.TreasureLevel);
                        break;
                    default:
                        break;
                }

                currentLevelIndex++;
            }
        }

        // Set the 0th level as the first one to enter.
        //nextObjects = new List<LevelObject>();
        //nextObjects.Add(levelObjects[1]);

        GenerateBattleLevel();
    }

    /// <summary>
    ///     Prevent repetitive code between switch cases.
    /// </summary>
    /// <param name="prefab">Prefab to instantiate.</param>
    /// <param name="level">Level that belongs to this object.</param>
    /// <param name="pos">Position for this object.</param>
    private void InstantiateLevel(GameObject prefab, Level level, LevelTypes levelType) {
        GameObject gameObject = Instantiate(prefab, transform);

        LevelObject levelObject = gameObject.GetComponent<LevelObject>();
        levelObject.SetUp(currentLevelIndex, levelType, level);
        level.AssignLevelObject(levelObject);

        //LevelObject newLevelObject = new LevelObject();
        //newLevelObject.SetUp(currentLevelIndex, levelType, level, this);
        //level.AssignLevelObject(newLevelObject);

        levelObjects.Add(levelObject);
    }

    /// <summary>
    ///     Get the level type of the next level and randomly generate the content.
    /// </summary>
    //public void StartNextLevel() {
    //    Debug.Log("Entering level: " + Levels[progression]);

    //    switch (Levels[progression]) {
    //        case LevelTypes.BattleLevel:
    //            GenerateBattleLevel();
    //            break;
    //        case LevelTypes.ShopLevel:
    //            GenerateShopLevel();
    //            break;
    //        case LevelTypes.TreasureLevel:
    //            GenerateTreasureLevel();
    //            break;
    //        default:
    //            Debug.Log("This aint no level fool");
    //            break;
    //    }
    //}

    /// <summary>
    ///     Start battle level.
    /// </summary>
    private void GenerateBattleLevel() {
        Debug.Log("Battle!");
        // play battle animation

        // Allow player to choose when to start encounter
        SceneManager.LoadScene("EnterLevel");
        
        // When they press enter.
        // SceneManager.LoadScene("Encounter");

        // When the specific scene is done, they will do LoadNextLevel();
    }

    /// <summary>
    ///     Start shop level.
    /// </summary>
    private void GenerateShopLevel() {
        Debug.Log("Shop!");
        // play shop animation

        // proceed to shop scene
        SceneManager.LoadScene("Shop");

        // When the specific scene is done, they will do LoadNextLevel();
    }

    /// <summary>
    ///     Start treasure level.
    /// </summary>
    private void GenerateTreasureLevel() {
        Debug.Log("Treasure!");
        // play treasure animation

        // proceed to treasure scene
        SceneManager.LoadScene("Treasure");
        
        // When the specific scene is done, they will do LoadNextLevel();
    }

    /// <summary>
    ///     Load the next scene based on the amount of next levels.
    /// </summary>
    public void LoadNextLevel() {
        // Boss room, load special extra cursed scene?
        if (currentLevelIndex + 1 == Levels.Count) {
            Debug.Log("Boss defeated lmao xd");
        }

        if (nextObjects.Count == 1 && nextObjects[0] != null) {
            LevelObject levelObject = nextObjects[0];
            LevelTypes levelType = nextObjects[0].LevelType;

            SetNextLevel(levelObject.CurrentLevelIndex);

            // No need to choose a path, load next level
            if (levelType == LevelTypes.BattleLevel) {
                GenerateBattleLevel();
            } else if (levelType == LevelTypes.ShopLevel) {
                GenerateShopLevel();
            } else {
                GenerateTreasureLevel();
            }

            Debug.Log("Waiting for enter.");
        } else if (nextObjects.Count == 2) {
            // load scene with choice between 2 paths
            // if theres more than 2 options, just ignore the 2+.
            SceneManager.LoadScene("Choose");

            Debug.Log("Waiting for choice.");
        }
    }

    /// <summary>
    ///     Choose one of two paths given.
    /// </summary>
    /// <param name="left">If the chosen path was the left one.</param>
    public void ChooseLevelLeft(bool left) {
        LevelObject levelObject;
        LevelTypes levelType;
        
        if (left) {
            levelObject = nextObjects[0];
            levelType = nextObjects[0].LevelType;

        } else {
            levelObject = nextObjects[1];
            levelType = nextObjects[0].LevelType;
        }

        SetNextLevel(levelObject.CurrentLevelIndex);

        if (levelType == LevelTypes.BattleLevel) {
            GenerateBattleLevel();
        }
        else if (levelType == LevelTypes.ShopLevel) {
            GenerateShopLevel();
        }
        else {
            GenerateTreasureLevel();
        }

        Debug.Log("Waiting for enter.");
    }

    /// <summary>
    ///     Set the levels that will follow the current one.
    /// </summary>
    /// <returns>Position of the next level object.</returns>
    private void SetNextLevel(int currentLevelIndex) {
        this.currentLevelIndex = currentLevelIndex;

        // Enable the new possible to-be selected levels to be interacted with.
        LevelObject levelObject = levelObjects[currentLevelIndex];
        nextObjects = new List<LevelObject>();
        foreach (Level level in levelObject.Level.NextLevels) {
            //level.Object.Next = true;
            Debug.Log("Level " + level.Object.CurrentLevelIndex + " is enabled.");

            nextObjects.Add(level.Object);
        }
    }

    public void RegenerateMap() {
        Destroy(this);
    }
}

public enum LevelTypes
{
    BattleLevel,
    ShopLevel,
    TreasureLevel
}