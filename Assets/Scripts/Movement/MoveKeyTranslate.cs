using UnityEngine;
using System.Collections;

/// <summary>
/// move by direct translation of object
/// </summary>
public class MoveKeyTranslate : MonoBehaviour {
    // cf: http://pixelnest.io/tutorials/2d-game-unity/table-of-contents/

    /// <summary>
    /// speed of movement
    /// </summary>
    public Vector2 moveSpeed = new Vector2(20, 20);

    void Update() {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveSpeed.x * inputX, moveSpeed.y * inputY);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }
	
}
