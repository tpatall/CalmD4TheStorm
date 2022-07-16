using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Overworld.Instance.PlayerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 nextLevelPosition = Overworld.Instance.GetNextLevel();
            MoveToNext(nextLevelPosition);
        }
    }

    /// <summary>
    ///     Animation of moving the player to the next level.
    ///     Should be called once and the animation should start separately.
    /// </summary>
    /// <param name="nextLevelPos">Vector3 position of the next level.</param>
    private void MoveToNext(Vector3 nextLevelPos) {
        transform.position = Vector3.MoveTowards(transform.position, nextLevelPos, 5f);

        // do this as animation in the overworld class maybe?
        Overworld.Instance.PlayerPosition = transform.position;
        Overworld.Instance.StartNextLevel();
    }
}
