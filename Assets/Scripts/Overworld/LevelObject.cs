using UnityEditor;
using UnityEngine;

/// <summary>
///     Store all necessary information about a level.
/// </summary>
public class LevelObject : MonoBehaviour
{
    public bool Next;

    public int CurrentLevelIndex;

    public LevelType LevelType { get; private set; }

    public Level Level { get; private set; }

    public void SetUp(int currentLevelIndex, LevelType levelType, Level level) {
        CurrentLevelIndex = currentLevelIndex;
        LevelType = levelType;
        Level = level;
    }
}