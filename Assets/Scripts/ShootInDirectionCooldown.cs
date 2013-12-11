using UnityEngine;
using System.Collections;

/// <summary>
/// shoot a bullet labeled with your own state
/// </summary>
[RequireComponent(typeof(FactionState))]
public class ShootInDirectionCooldown : ActionCooldown {

    /// <summary>
    /// prefab for object to shoot
    /// </summary>
    public Transform bulletPrefab;

    /// <summary>
    /// direction fired shot should travel relative to transform
    /// </summary>
    public Vector2 shootDirection;

    /// <summary>
    /// speed shot should travel
    /// </summary>
    public Vector2 shootSpeed;

    private FactionState myFaction;

	void Awake() {
        myFaction = gameObject.GetComponent<FactionState>();
	}
	
	override protected void Update () {
        base.Update(); // call basic updates from ActionCooldown

        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        shoot |= Input.GetButtonDown("Jump"); // default spacebar

        if (shoot) {
            Shoot(myFaction.faction);
        }
	}


    public void Shoot(Faction firingFaction) {
        // NOTE: only allows 1-v-many collision mappings
        // TODO: bullet is assigned single faction (could easily extend...)
        if (OffCD) {
            // set cooldown before next action
            timerCD = cooldown;

            Transform bullet = Instantiate(bulletPrefab) as Transform;

            bullet.position = transform.position;

            // assign movement
            MoveInDirection move = bullet.gameObject.GetComponent<MoveInDirection>();
            if (move != null) {
                move.moveDirection = shootDirection;
                move.moveSpeed = shootSpeed;
            }

            // assign faction
            FactionState bulletFaction = bullet.gameObject.GetComponent<FactionState>();
            if (bulletFaction != null) {
                bulletFaction.faction = firingFaction;
            }
        }
    }
}
