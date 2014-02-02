using UnityEngine;
using System.Collections;

/// <summary>
/// move by direct translation of object
/// </summary>
public class MoveKeyTranslate : Move {
    // cf: http://pixelnest.io/tutorials/2d-game-unity/table-of-contents/

    void Update() {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveSpeed.x * inputX, moveSpeed.y * inputY);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }
}
