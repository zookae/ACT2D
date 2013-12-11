using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ThresholdType {
    ABOVE,
    BELOW,
    EQUAL,
    NOTEQUAL
}

/// <summary>
/// manage self-destruction by resource value
/// </summary>
[RequireComponent(typeof(ResourceState))]
public class ResourceThresholdDestroy : MonoBehaviour {

    /// <summary>
    /// target resource type from object
    /// </summary>
    public ResourceType targetResourceType;

    /// <summary>
    /// resource state being monitored
    /// </summary>
    private List<ResourceState> targetResources;

    /// <summary>
    /// value for the threshold to monitor
    /// </summary>
    public float thresholdValue;

    /// <summary>
    /// type of threshold to impose
    /// </summary>
    public ThresholdType thresholdType;

    void Start() {
        targetResources = new List<ResourceState>();

        // get all matching resources
        ResourceState[] resources = gameObject.GetComponents<ResourceState>();
        foreach (ResourceState resource in resources) {
            if (resource.resourceType == targetResourceType) {
                targetResources.Add(resource);
            }
        }
    }

    void Update() {
        foreach (ResourceState tr in targetResources) {
            if (thresholdType == ThresholdType.ABOVE
            && tr.value > thresholdValue) {
                Destroy(gameObject);
            }
            else if (thresholdType == ThresholdType.BELOW
                && tr.value < thresholdValue) {
                Destroy(gameObject);
            }
            else if (thresholdType == ThresholdType.EQUAL
                && tr.value == thresholdValue) {
                Destroy(gameObject);
            }
            else if (thresholdType == ThresholdType.NOTEQUAL
                && tr.value != thresholdValue) {
                Destroy(gameObject);
            }
        }
    }
	
}
