using UnityEngine;
using System.Collections;

/// <summary>
/// NPC auto-firing
/// currently fixed rate
/// </summary>
public class NPCShootInDirectionCooldown : ShootCooldown {

    protected FactionState myFaction;

    void Awake() {
        myFaction = gameObject.GetComponent<FactionState>();
    }
	

    protected void Update() {
        UpdateCD();

        if (OffCD) {
            Transform bullet = Fire(myFaction.faction);
            MoveInDirection move = bullet.gameObject.AddComponent<MoveInDirection>();
            if (move != null) {
                move.moveSpeed = shotSpeed;
                move.moveDirection = shotDirection;
            }
        }
	}
}
