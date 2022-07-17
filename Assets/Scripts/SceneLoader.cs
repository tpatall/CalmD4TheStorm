using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByID(int id)
    {
        SceneManager.LoadScene(sceneBuildIndex: id);
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(sceneName: name);
    }
}
