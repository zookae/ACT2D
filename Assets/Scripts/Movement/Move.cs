using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    /// <summary>
    /// speed to move
    /// </summary>
    public Vector2 moveSpeed;

    /// <summary>
    /// Move toward a target position at a given speed
    /// </summary>
    /// <param name="moveTarget"></param>
    /// <param name="moveSpeed"></param>
    protected void MoveToTarget(Vector2 moveTarget, Vector2 moveSpeed) {
        Debug.Log("[Move] MoveRelative - at: " + (Vector2)transform.position);
        Debug.Log("[Move] MoveRelative - moving toward: " + moveTarget);
        transform.position = Vector2.MoveTowards((Vector2)transform.position, moveTarget, moveSpeed.magnitude * Time.deltaTime);
    }
}
