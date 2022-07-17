using UnityEditor;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public bool Next;

    public int CurrentLevelIndex;

    public LevelTypes LevelType { get; private set; }

    public Level Level { get; private set; }

    public void SetUp(int currentLevelIndex, LevelTypes levelType, Level level) {
        CurrentLevelIndex = currentLevelIndex;
        LevelType = levelType;
        Level = level;
    }
}