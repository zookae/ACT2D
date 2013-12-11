using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollideFactionChangeResourceTarget : MonoBehaviour {

    public Transform resourceHolder;

    /// <summary>
    /// faction resource change should affect if collided with
    /// </summary>
    public Faction targetFaction;

    /// <summary>
    /// resource on target to change
    /// </summary>
    public ResourceType selfResourceType;

    /// <summary>
    /// own resource to update
    /// </summary>
    private List<ResourceState> selfResources;

    /// <summary>
    /// amount to change target resource by
    /// </summary>
    public int changeAmount;

    void Start() {
        ResourceState[] resources = resourceHolder.GetComponents<ResourceState>();

        selfResources = new List<ResourceState>();

        // get own resources
        // NOTE: currently assumed fixed over duration of game
        foreach (ResourceState resource in resources) {
            if (resource.resourceType == selfResourceType) {
                selfResources.Add(resource);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        FactionState collidedFaction = collider.gameObject.GetComponent<FactionState>();

        // check if target has faction
        if (collidedFaction != null && collidedFaction.faction == targetFaction) {
            foreach (ResourceState sr in selfResources) {
                if (sr != null) {
                    sr.ChangeValue(sr.value + changeAmount);
                }
            }
        }
    }
}
