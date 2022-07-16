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

        PopulateMap();
        //PopulateLevels();
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

    private void PopulateMap() {
        Debug.Log("Total levels: " + Levels.Count);
        GenerateMap generateMap = new GenerateMap(Levels.Count);
        List<Branch> branches = generateMap.Branches;

        int levelID;
        // Build the map branch by branch (to avoid crossing paths).
        for (int i = 0; i < branches.Count; i++) {
            for (int j = 0; j < branches[i].Levels.Count; j++) {
                GameObject gameObject;

                // Choose the level type from the specified level types based on progression in the game.
                // With a chance to divert?
                levelID = branches[i].Levels[j].LevelID;

                Vector2 gameObjectPos = new Vector2(levelID * 3f, 0.5f - (2 * i));

                switch (Levels[levelID]) {
                    case LevelTypes.BattleLevel:
                        gameObject = Instantiate(battleLevelPrefab, transform);
                        gameObject.transform.position = gameObjectPos;
                        levelObjects.Add(gameObject);
                        break;
                    case LevelTypes.ShopLevel:
                        gameObject = Instantiate(shopLevelPrefab, transform);
                        gameObject.transform.position = gameObjectPos;
                        levelObjects.Add(gameObject);
                        break;
                    case LevelTypes.TreasureLevel:
                        gameObject = Instantiate(treasureLevelPrefab, transform);
                        gameObject.transform.position = gameObjectPos;
                        levelObjects.Add(gameObject);
                        break;
                    default:
                        break;
                }
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

public class GenerateMap
{
    public List<Branch> Branches { get; private set; }

    public GenerateMap(int levelLength) {
        Branches = new List<Branch>();

        GenerateRandomBranches(levelLength);
    }

    private void GenerateRandomBranches(int levelLength) {
        Branch mainBranch = new Branch(Branches.Count, null, null);
        mainBranch.AddLevel(0);
        Branches.Add(mainBranch);

        // LevelLength (and i) determine the progress throughout the game.
        for (int i = 1; i < levelLength; i++) {
            for (int j = 0; j < Branches.Count; j++) {
                if (Branches[j] == mainBranch) {
                    // Let the mainbranch split off a couple of times at a random or specific point.
                     if (i % 6 == 0 && i < 15) { // every 6 steps split off.
                        AddBranch(mainBranch, i);
                     }
                     else {
                        mainBranch.AddLevel(i);
                    }
                } 
                else {
                    // also branch out from non-main branches at a much lower chance and with some conditions.
                    if (Random.Range(0f, 1f) < 0.05f && i < 15) { // 1 in 100 chance to split off.
                        AddBranch(Branches[j], i);
                    }
                    else if (Branches[j].Continuing) Branches[j].AddLevel(i);
                }
            }
        }
    }

    /// <summary>
    ///     Adds a new branch to the list of branches, and also adds a level for the parent branch and the new branch.
    /// </summary>
    /// <param name="branch">The parent branch.</param>
    /// <param name="levelID">Current level ID.</param>
    private void AddBranch(Branch branch, int levelID) {
        Branch newBranch = new Branch(Branches.Count, branch, branch.CurrentLevel);
        Branches.Add(newBranch);

        List<Level> levels = new List<Level>();

        // Add level to branch
        Level branchLevel = branch.AddLevel(levelID);
        levels.Add(branchLevel);

        // Merge discontinued branches back into current
        if (branch.DiscontinuedChildren.Count > 0) {
            foreach (Level childLevel in branch.DiscontinuedChildren) {
                branchLevel.ReferencePrevLevel(childLevel);
                childLevel.ReferenceNextLevels(levels);
            }
        }

        // Add level to new branch
        Level newBranchlevel = newBranch.AddLevel(levelID);
        levels.Add(newBranchlevel);

        // Give the previous level references to the two newly created levels.
        branch.CurrentLevel.ReferenceNextLevels(levels);
    }

    //private void PopulateMap() {
    //    List<GameObject> levelObjects = new List<GameObject>();

    //    GameObject levelObject = new GameObject();
    //    Vector2 levelObjectPosition = new Vector2(0f, 0.5f);

    //    // Set the start level or level you leave from.
    //    levelObject.transform.position = levelObjectPosition;

    //    for (int i = 0; i < levelLength; i++) {
    //        levelObjectPosition = new Vector2(levelObjectPosition.x + 3f, levelObjectPosition.y);

    //        // Make a new levelObject for each active branch.
    //        for (int j = 0; j < branches.Count; j++) {
    //            Level level = branches[j].Levels.Find(l => l.LevelID == i);

    //            // If the branch has a level with the currently active ID.
    //            if (level != null) {
    //                // Last level in branch, so it has one next level.
    //                if (level.CurrentBranchID == branches.Count) {

    //                }
                    
    //                levelObject = new GameObject("", typeof(MapPoint));
    //                levelObject.transform.position = new Vector2(levelObjectPosition.x, levelObjectPosition.y + j);

    //                MapPoint mapPoint = levelObject.GetComponent<MapPoint>();
    //                mapPoint.SetUp();
    //            }
    //        }


    //    }
    //}
}

/// <summary>
///     Branch is a collection of levels.
/// </summary>
public class Branch
{
    /// <summary>
    ///     If this branch is still active.
    ///     If its inactive, make the next level merge into the parent branch and dont add more levels to this branch.
    /// </summary>
    public bool Continuing = true;
    
    /// <summary>
    ///     Could be useful later, but for now only used to discern the main branch from non-main branches.
    /// </summary>
    public int BranchID { get; set; }
    
    public Level CurrentLevel { get; set; }

    /// <summary>
    ///     Branch this level split from.
    /// </summary>
    public Branch ParentBranch { get; set; }

    public List<Level> DiscontinuedChildren { get; set; }
    
    /// <summary>
    ///     Levels in this branch.
    /// </summary>
    public List<Level> Levels { get; set; }

    public Branch(int branchID, Branch parentBranch, Level currentLevel) {
        BranchID = branchID;
        ParentBranch = parentBranch;

        Levels = new List<Level>();

        CurrentLevel = currentLevel;

        DiscontinuedChildren = new List<Level>();
    }

    /// <summary>
    ///     Add a level to the list of levels in this branch.
    ///     If this exceeds a certain number, merge the branch into where it came from.
    /// </summary>
    /// <param name="levelID">Progress ID of the level.</param>
    public Level AddLevel(int levelID) {
        Level level = new Level(levelID, Levels.Count, CurrentLevel);
        Levels.Add(level);
        
        if (BranchID != 0 && Levels.Count > 5) {
            Continuing = false;

            // Search for the parent branch that is still continuing.
            Branch parent = ParentBranch;
            while (!parent.Continuing) {
                parent = parent.ParentBranch;
            }
            // And put the current level in a waiting list to be added there.
            parent.DiscontinuedChildren.Add(level);
        }

        CurrentLevel = level;
        return level;
    }
}

public class Level
{
    /// <summary>
    ///     The ID of the vertical line of this level (to find out what levels are at the same progression in other branches).
    /// </summary>
    public int LevelID { get; set; }

    /// <summary>
    ///     The index of this level in the current branch.
    /// </summary>
    public int CurrentBranchID { get; set; }

    /// <summary>
    ///     If multiple, indicates it came from a branch merge.
    /// </summary>
    public List<Level> PreviousLevels { get; set; }

    /// <summary>
    ///     If multiple, indicates a branch gets split.
    /// </summary>
    public List<Level> NextLevels { get; set; }

    public Level(int levelID, int currentBranchID, Level previousLevel) {
        LevelID = levelID;
        CurrentBranchID = currentBranchID;

        PreviousLevels = new List<Level>();
        PreviousLevels.Add(previousLevel);
    }

    /// <summary>
    ///     Define the levels that follow this level.
    ///     This will only be called when the branch of this level will get a child.
    /// </summary>
    /// <param name="nextLevels">List of levels that follow from this level.</param>
    public void ReferenceNextLevels(List<Level> nextLevels) {
        NextLevels = nextLevels;
    }

    /// <summary>
    ///     Add more previous levels to this Level.
    ///     This will only be called when a child branch is merged into this branch at this Level.
    /// </summary>
    /// <param name="prevLevel">The level that gets merged in.</param>
    public void ReferencePrevLevel(Level prevLevel) {
        PreviousLevels.Add(prevLevel);
    }
}
