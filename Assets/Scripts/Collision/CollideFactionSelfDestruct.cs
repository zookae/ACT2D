using UnityEngine;
using System.Collections;


public class CollideFactionSelfDestruct : MonoBehaviour {

    /// <summary>
    /// faction resource change should affect if collided with
    /// </summary>
    public Faction targetFaction;

    void OnTriggerEnter2D(Collider2D collider) {
        FactionState collidedFaction = collider.gameObject.GetComponent<FactionState>();

        // check if target has faction
        if (collidedFaction != null && collidedFaction.faction == targetFaction) {
            Destroy(this.gameObject);
        }
    }
}
