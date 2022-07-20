using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [Tooltip("How many encounters per enemy pool (excluding first and last pool).")]
    public int LevelsPerPool = 3;

    public List<LevelInformation> levelObjects = new List<LevelInformation>();

    private int levelsPerPool;

    // Use this for initialization
    void Start() {
        levelsPerPool = LevelsPerPool;
    }

    // Current enemy pools in children:
    // - 1      First level
    // - 2,3,4  Easy
    // - 5,6,7  Medium
    // - 8,9,10 Hard
    // - 11     Boss

    // Current shop division:
    // - 1,2,3 shop
    // - 4,5,6 shop
    // - 7,8,9 shop
    // - 10,11 boss

    public List<LevelInformation> BuildLevelList() {
        int totalPools = transform.childCount;
        // Total number of pools minus the first and last pool.
        int totalBattleLevels = (totalPools - 2) * levelsPerPool + 2;

        for (int i = 0; i < totalBattleLevels; i++) {
            // Add shops at specific intervals
            if (i == levelsPerPool || i == levelsPerPool * 2|| i == levelsPerPool * 3) {
                LevelInformation shopLevelObject = new LevelInformation(0, null, LevelType.ShopLevel);
                levelObjects.Add(shopLevelObject);
            }

            AddBattleLevel(i);
        }

        LevelInformation endLevelObject = new LevelInformation(0, null, LevelType.EndLevel);
        levelObjects.Add(endLevelObject);

        return levelObjects;
    }

    /// <summary>
    ///     Add an 'EnterLevel' and battle level to the list of levels.
    /// </summary>
    private void AddBattleLevel(int index) {
        EnemyPool[] pools;
        EnemyPool pool;

        int level = index;
        // First level - is only 1 but still
        if (level == 0) {
            pools = transform.GetChild(0).GetComponents<EnemyPool>();
            pool = pools[Random.Range(0, pools.Length)];
        }
        // Easy
        else if (level <= levelsPerPool) {
            pools = transform.GetChild(1).GetComponents<EnemyPool>();
            pool = pools[Random.Range(0, pools.Length)];
        }
        // Medium
        else if (level <= levelsPerPool * 2) {
            pools = transform.GetChild(2).GetComponents<EnemyPool>();
            pool = pools[Random.Range(0, pools.Length)];
        } 
        // Hard
        else if (level <= levelsPerPool * 3) {
            pools = transform.GetChild(3).GetComponents<EnemyPool>();
            pool = pools[Random.Range(0, pools.Length)];
        }
        // Boss - is only 1 but still
        else {
            pools = transform.GetChild(4).GetComponents<EnemyPool>();
            pool = pools[Random.Range(0, pools.Length)];
        }

        int reward = pool.reward;
        List<GameObject> enemies = pool.enemyPool;
        LevelInformation battleLevelObject = new LevelInformation(reward, enemies, LevelType.BattleLevel);

        LevelInformation enterLevelObject = new LevelInformation(0, null, LevelType.EnterLevel);
        levelObjects.Add(enterLevelObject);
        levelObjects.Add(battleLevelObject);
    }
}

public class LevelInformation
{
    /// <summary>
    ///     Money reward for winning this battle.
    /// </summary>
    public int Reward { get; set; }

    /// <summary>
    ///     List of enemies taken from the relevant enemy pool.
    /// </summary>
    public List<GameObject> Enemies { get; set; }

    public LevelType LevelType { get; set; }

    public LevelInformation(int reward, List<GameObject> enemies, LevelType levelType) {
        Reward = reward;
        Enemies = enemies;
        LevelType = levelType;
    }

}