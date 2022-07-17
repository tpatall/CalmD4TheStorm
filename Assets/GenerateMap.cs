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

    /// <summary>
    ///     Generate random values that decide when the main branch will split off.
    /// </summary>
    /// <returns>An array of possible split numbers.</returns>
    private int[] SplittingOptions() {
        int firstSplit = (int)Mathf.Floor(Random.Range(3, 6));
        int secondSplit = (int)Mathf.Floor(Random.Range(9, 13));

        int[] splittingOptions = new int[1];
        splittingOptions[0] = firstSplit;
        //splittingOptions[1] = secondSplit;

        return splittingOptions;
    }

    private void GenerateRandomBranches(int levelLength) {
        int[] splittingOptions = SplittingOptions();
        
        Branch mainBranch = new Branch(null);
        mainBranch.AddLevel(0);
        Branches.Add(mainBranch);

        // LevelLength (and i) determine the progress throughout the game.
        for (int i = 1; i < levelLength; i++) {
            for (int j = 0; j < Branches.Count; j++) {
                if (Branches[j] == mainBranch) {
                    // Let the mainbranch split off a couple of times at a random point.
                    if (i == splittingOptions[0]) { // || i == splittingOptions[1]) {
                        Branch newBranch = new Branch(mainBranch);
                        Branches.Add(newBranch);

                        AddLevel(mainBranch, i);
                    }
                    else {
                        AddLevel(mainBranch, i);
                    }
                }
                else if (Branches[j].IsActive) {
                    // also branch out from non-main branches at a much lower chance and only before a certain point.
                    if (Random.Range(0f, 1f) < 0.1f && i < 6) {
                        Branch newBranch = new Branch(Branches[j]);
                        Branches.Add(newBranch);
                        
                        AddLevel(Branches[j], i);
                    }
                    else AddLevel(Branches[j], i);
                }
            }
        }
    }

    private void AddLevel(Branch branch, int levelID) {
        // Add level to branch
        Level newLevel = branch.AddLevel(levelID);

        MergeChildren(branch, newLevel);
    }

    /// <summary>
    ///     Merge discontinued branches back into current.
    /// </summary>
    private void MergeChildren(Branch branch, Level newLevel) {
        if (branch.ChildrenToBeMerged.Count > 0) {
            for (int i = 0; i < branch.ChildrenToBeMerged.Count; i++) {
                Level childLevel = branch.ChildrenToBeMerged[i];

                childLevel.ReferenceNextLevel(newLevel);

                branch.ChildrenToBeMerged.Remove(childLevel);
            }
        }
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
    public bool IsActive = true;

    /// <summary>
    ///     Previous level from maybe previous branch used for map generation.
    /// </summary>
    public Level PreviousLevel { get; set; }

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

    public Branch(Branch parentBranch) {
        ParentBranch = parentBranch;

        if (parentBranch != null) {
            PreviousLevel = parentBranch.CurrentLevel;
            CurrentLevel = parentBranch.CurrentLevel;
        } else {
            PreviousLevel = null;
            CurrentLevel = null;
        }

        Levels = new List<Level>();
        ChildrenToBeMerged = new List<Level>();
    }

    /// <summary>
    ///     Add a level to the list of levels in this branch.
    ///     If this exceeds a certain number, merge the branch into where it came from.
    /// </summary>
    /// <param name="newLevelID">Progress ID of the level.</param>
    public Level AddLevel(int newLevelID) {
        Level newLevel;
        // If first level in new branch
        if (CurrentLevel == null) {
            newLevel = new Level(newLevelID, CurrentLevel, PreviousLevel);
            // PreviousLevel remains null
        } else {
            newLevel = new Level(newLevelID, CurrentLevel, PreviousLevel);
            PreviousLevel = CurrentLevel;
        }
        Levels.Add(newLevel);

        // If not main branch (as mainbranch has no parent)
        // Then stop the branch at a random interval between 3 and 7 (exclusive 7).
        if (ParentBranch != null && Levels.Count > Random.Range(3, 5)) {
            IsActive = false;

            // Search for the parent branch that is still active.
            Branch parent = ParentBranch;
            while (!parent.IsActive) {
                parent = parent.ParentBranch;
            }
            // And put the current level in a waiting list to be added there.
            parent.ChildrenToBeMerged.Add(newLevel);
        }

        CurrentLevel = newLevel;
        return newLevel;
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
    ///     If multiple, indicates a branch gets split.
    /// </summary>
    public List<Level> NextLevels { get; set; }

    public Level(int levelID, Level previousLevel, Level parentBranchLevel) {
        LevelID = levelID;

        if (previousLevel != null) {
            previousLevel.ReferenceNextLevel(this);
        }
        // If previousLevel == null this is the first level in a new branch.
        // So add a reference to the level from the parent branch.
        else if (parentBranchLevel != null) {
            parentBranchLevel.ReferenceNextLevel(this);
        }

        NextLevels = new List<Level>();
    }

    public void ReferenceNextLevel(Level nextLevel) {
        NextLevels.Add(nextLevel);
    }

    /// <summary>
    ///     Assign level object after generation.
    /// </summary>
    /// <param name="levelObject">GameObject on the map.</param>
    public void AssignLevelObject(LevelObject levelObject) {
        Object = levelObject;
    }
}