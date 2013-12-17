using UnityEngine;
using System.Collections;

public class ShootCooldown : Shoot {

    public override Transform Fire(Faction firingFaction) {
        // NOTE: only allows 1-v-many collision mappings
        // TODO: bullet is assigned single faction (could easily extend...)
        if (OffCD) {
            // set cooldown before next action
            timerCD = cooldown;

            Transform bullet = Instantiate(shotPrefab) as Transform;

            bullet.position = transform.position;


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
