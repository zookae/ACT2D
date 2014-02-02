using UnityEngine;
using System.Collections;

public class PatrolRelative : Move {

    public Vector2[] positions;

    public int currentPosition;
    public Vector2 targetPosition;
    private float minDistance = 0.1f;

    /// <summary>
    /// initial position to return to for patrolling
    /// </summary>
    public Vector2 initPosition;

	// Use this for initialization
	void Start () {

        currentPosition = 0;
        initPosition = (Vector2)transform.position;
        if (positions.Length < 1) {
            Debug.LogError("[PatrolRelative] Start - no positions to move to!");
        }

        // set positions to relative coordinates
        for( int i = 0; i < positions.Length; i++ ) {
            positions[i] = (Vector2)transform.position + positions[i];
        }

        targetPosition = positions[currentPosition];
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance((Vector2)transform.position, targetPosition) < minDistance) {
            if (currentPosition+1 < positions.Length) {
                // move to next location
                currentPosition++;
            }
            else {
                // return to first location
                currentPosition = 0;
            }
            targetPosition = positions[currentPosition];
        }
        MoveToTarget(targetPosition, moveSpeed);
	}
}
