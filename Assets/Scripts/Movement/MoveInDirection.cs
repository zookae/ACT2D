using UnityEngine;
using System.Collections;

public class MoveInDirection : Move {

    /// <summary>
    /// direction of movement
    /// </summary>
    public Vector2 moveDirection = new Vector2(0, 1);
	
	void Update() {
        Vector2 movement = new Vector2(moveSpeed.x * moveDirection.x, moveSpeed.y * moveDirection.y);
        movement *= Time.deltaTime;
        transform.Translate(movement);
	}
}
