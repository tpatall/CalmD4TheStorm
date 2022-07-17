using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Overworld overworld;

    // Start is called before the first frame update
    void Start()
    {
        overworld = Overworld.Instance;

        transform.position = overworld.PlayerPosition;
    }

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space) && overworld.CurrentState == GameState.WaitForEnter) {
    //        overworld.StartNextLevel();
    //    }
    //}

    /// <summary>
    ///     Animation of moving the player to the next level.
    ///     Should be called once and the animation should start separately.
    /// </summary>
    public void MoveToNext(Vector3 nextLevelPosition) {
        transform.position = Vector3.MoveTowards(transform.position, nextLevelPosition, 50f);

        // do this as animation in the overworld class maybe?
        overworld.PlayerPosition = transform.position;
    }
}
