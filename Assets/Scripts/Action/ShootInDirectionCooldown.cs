using UnityEngine;
using System.Collections;

/// <summary>
/// shoot a bullet labeled with your own state
/// </summary>
[RequireComponent(typeof(FactionState))]
public class ShootInDirectionCooldown : Shoot {

    protected FactionState myFaction;

	void Awake() {
        myFaction = gameObject.GetComponent<FactionState>();
	}
	
	protected void Update () {
        UpdateCD(); // call basic updates from ActionCooldown

        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        shoot |= Input.GetButtonDown("Jump"); // default spacebar

        if (shoot) {
            Fire(myFaction.faction); // fire bullet
        }
	}


    public override Transform Fire(Faction firingFaction) {
        // NOTE: only allows 1-v-many collision mappings
        // TODO: bullet is assigned single faction (could easily extend...)
        if (OffCD) {
            // set cooldown before next action
            timerCD = cooldown;

            Transform bullet = Instantiate(shotPrefab) as Transform;

            bullet.position = transform.position;

            // assign movement
            MoveInDirection move = bullet.gameObject.GetComponent<MoveInDirection>();
            if (move != null) {
                move.moveDirection = shotDirection;
                move.moveSpeed = shotSpeed;
            }

            // assign faction
            FactionState bulletFaction = bullet.gameObject.GetComponent<FactionState>();
            if (bulletFaction != null) {
                bulletFaction.faction = firingFaction;
            }

            return bullet;
        }
        return null;
    }
}
