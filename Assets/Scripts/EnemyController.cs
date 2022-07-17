using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public static EnemyController Instance;

    public List<Enemy> currEnemies;

    public GameObject defaultEnemy;

    public Vector2 spawnPosition;
    public Vector2 offset;

    private void Start() {
        Instance = this;
    }

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

    public void Kill(Enemy enemy) {
        currEnemies.Remove(enemy);

        Destroy(enemy.gameObject);

        if(currEnemies.Count == 0) {
            Debug.Log("All enemies are dead!");
        }


    }
}
