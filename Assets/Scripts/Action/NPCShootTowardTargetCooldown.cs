using UnityEngine;
using System.Collections;

/// <summary>
/// NPC auto-firing
/// currently fixed rate
/// </summary>
public class NPCShootTowardTargetCooldown : ShootCooldown {

    public Transform shotTarget;

    protected FactionState myFaction;
    
    void Awake() {
        myFaction = gameObject.GetComponent<FactionState>();
        shotTarget = GameObject.Find(shotTarget.name).transform; // HACK: assume target is only entity w/that name and get it
    }
	

    protected void Update() {
        UpdateCD();

        if (OffCD && shotTarget != null) {
            Transform bullet = Fire(myFaction.faction);
            MoveTowardTarget move = bullet.gameObject.AddComponent<MoveTowardTarget>();
            if (move != null) {
                move.moveSpeed = shotSpeed;
                move.target = shotTarget;
            }
        }
	}
}
