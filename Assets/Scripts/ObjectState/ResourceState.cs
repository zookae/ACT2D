using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// possible types of resources to manage
/// </summary>
public enum ResourceType {
    Health,
    Score
}

public class ResourceState : MonoBehaviour {

    /// <summary>
    /// type of resource being tracked
    /// </summary>
    public ResourceType resourceType;


    /// <summary>
    /// amount of resource possessed
    /// </summary>
    public int value;

    /// <summary>
    /// minimum possible value for resource
    /// </summary>
    public int minValue = 0;

    /// <summary>
    /// maximum possible value for resource
    /// </summary>
    public int maxValue = int.MaxValue;

    /// <summary>
    /// update resource value; fixed w/in [min,max]
    /// </summary>
    /// <param name="newValue"></param>
    public void ChangeValue(int newValue) {
        value = Mathf.Clamp(newValue, minValue, maxValue);
    }
}
