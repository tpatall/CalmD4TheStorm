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
    
    [SerializeField] private GameObject battleLevelPrefab;
    [SerializeField] private GameObject shopLevelPrefab;
    [SerializeField] private GameObject treasureLevelPrefab;

    [SerializeField] private List<LevelTypes> Levels = new List<LevelTypes>();

    /// <summary>
    ///     List of level objects in scene.
    ///     Used to get information about next level.
    /// </summary>
    private List<GameObject> levelObjects = new List<GameObject>();

    /// <summary>
    ///     Current index in the list of levels the player is at.
    /// </summary>
    private int currentLevelIndex = 1;

    void Start()
    {
        currentLevelIndex = StartLevel > 1 ? StartLevel : 1;

        PopulateLevels();
    }

    /// <summary>
    ///     Populate map with level objects and fill in the levelObjects list.
    /// </summary>
    private void PopulateLevels() {
        // Do we need splt paths? The path can split up a maximum of 2 times. 
        // int pathSplits = 2;

        float previousYCoordinate = 0f;
        for (int i = 0; i < Levels.Count; i++) {
            GameObject gameObject;

            float chance = Random.Range(0f, 1f);
            // At a 40% move the path vertically, to simulate COMPLEX_LEVEL_DESIGN.
            if (chance < 0.3f) 
                previousYCoordinate -= 1f;
            // Only move up if the y coordinate is already below 0, to account for the lack of a space program.
            else if (chance < 0.4f && previousYCoordinate < -0.1f) 
                previousYCoordinate += 0.5f; 

            // Generate the next level position based on the previous position.
            Vector2 levelPosition = new Vector2(
                levelObjects.Count * 3,
                previousYCoordinate);

            switch (Levels[i]) {
                case LevelTypes.BattleLevel:
                    gameObject = Instantiate(battleLevelPrefab, transform);
                    gameObject.transform.position = levelPosition;
                    levelObjects.Add(gameObject);
                    break;
                case LevelTypes.ShopLevel:
                    gameObject = Instantiate(shopLevelPrefab, transform);
                    gameObject.transform.position = levelPosition;
                    levelObjects.Add(gameObject);
                    break;
                case LevelTypes.TreasureLevel:
                    gameObject = Instantiate(treasureLevelPrefab, transform);
                    gameObject.transform.position = levelPosition;
                    levelObjects.Add(gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    ///     Get the level type of the next level and randomly generate the content.
    /// </summary>
    public void StartNextLevel() {
        switch (Levels[currentLevelIndex]) {
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
    }


    /// <summary>
    ///     Start shop level.
    /// </summary>
    private void GenerateShopLevel() {
        // play shop animation

        // proceed to shop class
    }

    /// <summary>
    ///     Start treasure level.
    /// </summary>
    private void GenerateTreasureLevel() {
        // play treasure animation

        // proceed to treasure class
    }

    /// <summary>
    ///     Get the next level from the list of levels and update the index.
    /// </summary>
    /// <returns>Position of the next level object.</returns>
    public Vector3 GetNextLevel() {
        Vector3 nextLevelPosition = levelObjects[currentLevelIndex].transform.position;
        
        currentLevelIndex++;

        return nextLevelPosition;
    }
}

enum LevelTypes
{
    BattleLevel,
    ShopLevel,
    TreasureLevel
}
