using UnityEngine;
using System.Collections;

public class MoveTowardTarget : Move {
    
    /// <summary>
    /// movement target
    /// </summary>
    public Transform target;
    
    public Vector2 moveTarget;
    private Vector2 moveVector;

	// Use this for initialization
	void Start () {
        moveTarget = (Vector2)target.transform.position;
        moveVector = moveTarget - (Vector2)transform.position;
        moveVector = moveVector.normalized; // just want direction
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movement = new Vector2(moveSpeed.x * moveVector.x, moveSpeed.y * moveVector.y);
        movement *= Time.deltaTime;
        transform.Translate(movement);
	}
}
