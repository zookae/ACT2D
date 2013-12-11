using UnityEngine;
using System.Collections;

/// <summary>
/// NPC auto-firing
/// currently fixed rate
/// </summary>
public class NPCShootInDirectionCooldown : ShootInDirectionCooldown, IShoot {

    protected void Update() {
        UpdateCD();

        // just try to shoot, ignore inputs
        Shoot(myFaction.faction);
	}
}
