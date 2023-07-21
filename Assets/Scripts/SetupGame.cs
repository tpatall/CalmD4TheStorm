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
        GameObject musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        if (!musicPlayer) {
            Instantiate(MusicPlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

        // Destroy any existing prefabs that can contain saved information.
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (gameManager) {
            Destroy(gameManager);
        }
        
        GameObject playerInformation = GameObject.FindGameObjectWithTag("PlayerInformation");
        if (playerInformation) {
            Destroy(playerInformation);
        }
    }

    private void Start()
    {
        Instantiate(GameManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(PlayerInformationPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void PlayButton() {
        GameManager.Instance.BuildWorld();
    }
}
