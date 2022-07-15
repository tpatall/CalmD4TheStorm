using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public List<Enemy> currEnemies;

    public GameObject defaultEnemy;

    public Vector2 spawnPosition;
    public Vector2 offset;

    public void SpawnEnemies(List<GameObject> enemyObjects) {
        if(enemyObjects == null) {
            enemyObjects = new List<GameObject>() { defaultEnemy, defaultEnemy };
        }

        Debug.Log("Spawning " + enemyObjects.Count + " enemies.");

        Vector2 spawnPosition = this.spawnPosition;

        foreach(GameObject gameObject in enemyObjects) {
            GameObject newObject = Instantiate(gameObject, spawnPosition, Quaternion.identity, transform);
            Enemy newEnemy = newObject.GetComponent<Enemy>();
            currEnemies.Add(newEnemy);

            newEnemy.Initialize();

            spawnPosition += offset;
        }
    }
}
