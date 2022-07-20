using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelType
{
    EnterLevel,
    BattleLevel,
    ShopLevel,
    TreasureLevel,
    EndLevel
}

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField]
    private LevelGeneration levelGeneration;

    public List<LevelInformation> LevelObjects { get; set; }

    public int CurrentLevelIndex { get; set; }

    void Start()
    {
        CurrentLevelIndex = 0;
    }

    public void BuildWorld() {
        LevelObjects = levelGeneration.BuildLevelList();

        NextLevel();
    }

    public void NextLevel() {
        LevelType levelType = LevelObjects[CurrentLevelIndex++].LevelType;

        switch (levelType) {
            case LevelType.EnterLevel:
                Debug.Log("Entering enter level.");

                MusicPlayer.Instance.PlayMapMusic();
                SceneManager.LoadScene("EnterLevel");
                break;
            case LevelType.BattleLevel:
                Debug.Log("Entering battle level.");

                MusicPlayer.Instance.PlayBattleMusic();
                SceneManager.LoadScene("BattleScene");
                break;
            case LevelType.ShopLevel:
                Debug.Log("Entering shop level.");

                MusicPlayer.Instance.PlayMapMusic();
                SceneManager.LoadScene("Shop");
                break;
            case LevelType.TreasureLevel:
                Debug.Log("Entering treasure level.");
                
                MusicPlayer.Instance.PlayMapMusic();
                SceneManager.LoadScene("Treasure");
                break;
            case LevelType.EndLevel:
                Debug.Log("Entering ending level.");

                MusicPlayer.Instance.PlayMapMusic();
                SceneManager.LoadScene("Ending");
                break;
            default:
                break;
        }
    }

    public void GameOver() {
        MusicPlayer.Instance.PlayGameOverMusic();

        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    ///     Go back to title screen, when either pressing it on the 'Game Over'-screen or the 'Ending'-screen.
    /// </summary>
    public void ResetWorld() {
        MusicPlayer.Instance.PlayTitleMusic();

        Destroy(PlayerInformation.Instance);
        Destroy(this);
    }

    private void OnDestroy() {
        SceneManager.LoadScene("TitleScene");
    }
}