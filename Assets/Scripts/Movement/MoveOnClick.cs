using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Move))]
public class MoveOnClick : MonoBehaviour {

    public bool hasClicked = false;

    public Move[] moveControllers;

	void Awake() {
        // Freeze object movement to start
        moveControllers = gameObject.GetComponents<Move>();
        foreach( Move mc in moveControllers ) {
            //mc.isRunning = false;
            mc.enabled = false;
        }
	}
	
	void Update () {

        // allow movement on click
        if (!hasClicked && Input.GetMouseButtonDown(0)) {
            foreach( Move mc in moveControllers ) {
                //mc.isRunning = true;
                mc.enabled = true;
            }

            hasClicked = true;
        }
	}
}
