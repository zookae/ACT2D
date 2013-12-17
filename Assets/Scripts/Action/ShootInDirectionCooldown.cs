using UnityEngine;
using System.Collections;

/// <summary>
/// shoot a bullet labeled with your own state
/// </summary>
[RequireComponent(typeof(FactionState))]
public class ShootInDirectionCooldown : ShootCooldown {

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
            Transform bullet = Fire(myFaction.faction); // fire bullet
            if (bullet != null) {
                // assign movement
                MoveInDirection move = bullet.gameObject.AddComponent<MoveInDirection>();
                if (move != null) {
                    move.moveDirection = shotDirection;
                    move.moveSpeed = shotSpeed;
                }
            }
        }
	}

}
