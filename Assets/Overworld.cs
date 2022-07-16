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
        LevelObject levelObject = levelObjects[currentLevelIndex];
        foreach (Level level in levelObject.Level.NextLevels) {
            level.Object.Next = false;
        }

        this.currentLevelIndex = currentLevelIndex;

        // Enable the new possible to-be selected levels to be interacted with.
        levelObject = levelObjects[currentLevelIndex];
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
    
    /// <summary>
    ///     Current level used for map generation.
    /// </summary>
    public Level CurrentLevel { get; set; }

    /// <summary>
    ///     Branch this level split from.
    /// </summary>
    public Branch ParentBranch { get; set; }

    /// <summary>
    ///     Levels that wish for this branch to adopt them.
    /// </summary>
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
    ///     GameObject tied to this level.
    /// </summary>
    public LevelObject Object { get; set; } 

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

    /// <summary>
    ///     Assign level object after generation.
    /// </summary>
    /// <param name="gameObject">GameObject on the map.</param>
    public void AssignLevelObject(LevelObject levelObject) {
        Object = levelObject;
    }
}
