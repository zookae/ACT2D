using UnityEngine;
using System.Collections;

public abstract class Shoot : ActionCooldown, IShoot {

    /// <summary>
    /// prefab to use for fired objects
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// speed of fired objects to travel
    /// </summary>
    public Vector2 shotSpeed;

    /// <summary>
    /// direction fired objects should move in
    /// </summary>
    public Vector2 shotDirection;

    public abstract Transform Fire(Faction myFaction);
}
