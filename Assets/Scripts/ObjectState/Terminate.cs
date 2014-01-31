using UnityEngine;
using System.Collections;

public class Terminate : MonoBehaviour {

    /// <summary>
    /// State to terminate the game in when time exceeds maximum time
    /// </summary>
    public State termState;

    /// <summary>
    /// Threshold to compare desired value against
    /// </summary>
    public float valueThreshold;

    /// <summary>
    /// Type of threshold comparison (above vs below)
    /// </summary>
    public ThresholdType threshType;
    
    /// <summary>
    /// Test whether game is past maximum allowed time
    /// </summary>
    /// <returns></returns>
    public bool PassedTime(float timeThreshold) {
        if (GameState.Singleton.timeUsed >= timeThreshold) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Test whether given value has above or below relation to threshold value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="thresh"></param>
    /// <param name="threshType"></param>
    /// <returns></returns>
    public bool PassedThresh(float value, float thresh, ThresholdType threshType) {
        if (threshType == ThresholdType.ABOVE && value >= thresh) {
            return true;
        }
        else if (threshType == ThresholdType.BELOW && value <= thresh) {
            return true;
        }
        return false;
    }
}
