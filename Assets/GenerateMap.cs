using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                        AddBranchAndLevel(mainBranch, i);
                    }
                    else {
                        AddLevel(mainBranch, i);
                    }
                }
                else {
                    // also branch out from non-main branches at a much lower chance and with some conditions.
                    if (Random.Range(0f, 1f) < 0.05f && i < 15) { // 1 in 100 chance to split off.
                        AddBranchAndLevel(Branches[j], i);
                    }
                    else if (Branches[j].Continuing)
                        AddLevel(Branches[j], i);
                }
            }
        }
    }

    /// <summary>
    ///     Adds a new branch to the list of branches, and also adds a level for the parent branch and the new branch.
    /// </summary>
    /// <param name="branch">The parent branch.</param>
    /// <param name="levelID">Current level ID.</param>
    private void AddBranchAndLevel(Branch branch, int levelID) {
        Branch newBranch = new Branch(Branches.Count, branch, branch.CurrentLevel);
        Branches.Add(newBranch);

        Level currentLevel = branch.CurrentLevel;

        List<Level> levels = new List<Level>();

        // Add level to branch
        Level branchLevel = branch.AddLevel(levelID);
        levels.Add(branchLevel);

        // Merge discontinued branches back into current
        if (branch.ChildrenToBeMerged.Count > 0) {
            for (int i = 0; i < branch.ChildrenToBeMerged.Count; i++) {
                Level childLevel = branch.ChildrenToBeMerged[i];

                childLevel.ReferenceNextLevels(levels);

                branch.ChildrenToBeMerged.Remove(childLevel);
            }
        }

        // Give the previous level references to the two newly created levels.
        currentLevel.ReferenceNextLevels(levels);
    }

    private void AddLevel(Branch branch, int levelID) {
        List<Level> levels = new List<Level>();

        Level currentLevel = branch.CurrentLevel;

        // Add level to branch
        Level branchLevel = branch.AddLevel(levelID);
        levels.Add(branchLevel);

        // Merge discontinued branches back into current
        if (branch.ChildrenToBeMerged.Count > 0) {
            for (int i = 0; i < branch.ChildrenToBeMerged.Count; i++) {
                Level childLevel = branch.ChildrenToBeMerged[i];

                childLevel.ReferenceNextLevels(levels);

                branch.ChildrenToBeMerged.Remove(childLevel);
            }
        }

        currentLevel.ReferenceNextLevels(levels);
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
    public List<Level> ChildrenToBeMerged { get; set; }

    /// <summary>
    ///     Levels in this branch.
    /// </summary>
    public List<Level> Levels { get; set; }

    public Branch(int branchID, Branch parentBranch, Level currentLevel) {
        BranchID = branchID;
        ParentBranch = parentBranch;

        Levels = new List<Level>();

        CurrentLevel = currentLevel;

        ChildrenToBeMerged = new List<Level>();
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
            parent.ChildrenToBeMerged.Add(level);
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
    ///     If multiple, indicates a branch gets split.
    /// </summary>
    public List<Level> NextLevels { get; set; }

    public Level(int levelID, int currentBranchID, Level previousLevel) {
        LevelID = levelID;
        CurrentBranchID = currentBranchID;

        NextLevels = new List<Level>();

        if (previousLevel != null) {
            previousLevel.ReferenceNextLevel(this);
        }
    }

    /// <summary>
    ///     Define the levels that follow this level.
    ///     This will only be called when the branch of this level will get a child.
    /// </summary>
    /// <param name="nextLevels">List of levels that follow from this level.</param>
    public void ReferenceNextLevels(List<Level> nextLevels) {
        foreach (Level l in nextLevels) {
            NextLevels.Add(l);
        }
    }

    public void ReferenceNextLevel(Level nextLevel) {
        NextLevels.Add(nextLevel);
    }

    /// <summary>
    ///     Assign level object after generation.
    /// </summary>
    /// <param name="gameObject">GameObject on the map.</param>
    public void AssignLevelObject(LevelObject levelObject) {
        Object = levelObject;
    }
}