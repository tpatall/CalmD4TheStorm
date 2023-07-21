using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupGame : MonoBehaviour
{
    public GameObject GameManagerPrefab;
    public GameObject MusicPlayerPrefab;
    public GameObject PlayerInformationPrefab;

    private void Awake()
    {
        // If the persistent prefabs have already been instantiated, do not instantiate them again.
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
        if (!gameObject) {
            Instantiate(GameManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(MusicPlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(PlayerInformationPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void PlayButton() {
        GameManager.Instance.BuildWorld();
    }
}
