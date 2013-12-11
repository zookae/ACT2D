using UnityEngine;
using System.Collections;

public class CollideFactionChangeResource : MonoBehaviour {

    /// <summary>
    /// faction resource change should affect if collided with
    /// </summary>
    public Faction targetFaction;

    /// <summary>
    /// resource on target to change
    /// </summary>
    public ResourceType targetResource;

    /// <summary>
    /// amount to change target resource by
    /// </summary>
    public int changeAmount;

    void OnTriggerEnter2D(Collider2D collider) {
        FactionState collidedFaction = collider.gameObject.GetComponent<FactionState>();

        // check if target has faction
        if (collidedFaction != null && collidedFaction.faction == targetFaction) {
            // check if target has resource
            ResourceState[] collidedResources = collider.gameObject.GetComponents<ResourceState>();
            if (collidedResources != null) {
                foreach (ResourceState cr in collidedResources) {
                    cr.ChangeValue(cr.value + changeAmount);
                }
            }
        }
    }
}
