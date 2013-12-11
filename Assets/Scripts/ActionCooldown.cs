using UnityEngine;
using System.Collections;

/// <summary>
/// generic base timer for actions with cooldown
/// </summary>
public class ActionCooldown : MonoBehaviour {

    /// <summary>
    /// delay imposed between actions
    /// </summary>
    public float cooldown;

    /// <summary>
    /// time remaining on cooldown since last action
    /// </summary>
    protected float timerCD = 0f;

    virtual protected void Update() {
        // reduce by time passed
        if (timerCD > 0) {
            timerCD -= Time.deltaTime;
        }
    }

    /// <summary>
    /// is cooldown done?
    /// </summary>
    protected bool OffCD {
        get {
            return timerCD <= 0f;
        }
    }
}
