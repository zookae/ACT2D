using UnityEngine;
using System.Collections;

public class TerminateScoreAtTime : Terminate {

    public float MaxTime;

    // Update is called once per frame
	void Update () {
        if (PassedTime(MaxTime) &&
            PassedThresh(GameState.Singleton.score, valueThreshold, ThresholdType.ABOVE)) {
                //Debug.Log("[TerminateScoreAtTime] winner!");
            GameState.Singleton.CurrentState = State.Win; // win if you are above score threshold at max time
        }
        else if (PassedTime(MaxTime)) {
            //Debug.Log("[TerminateScoreAtTime] you lost!");
            GameState.Singleton.CurrentState = State.Lose; // lose if you run out of time and weren't above threshold
        }
	}
}
