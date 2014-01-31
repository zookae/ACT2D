using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LikertChangeTag : MonoBehaviour {

    /// <summary>
    /// Tag of entity that colliding with triggers change
    /// </summary>
    public string targetTag;

    /// <summary>
    /// Type of the resource to change
    /// </summary>
    public LikertScale rtype;

    /// <summary>
    /// Global state tracking all likert answers. 
    ///     Should likely be factored into per-object tracking
    /// </summary>
    private TrackLikert tracker;


    void Start() {
        if (tracker == null) {
            tracker = GameObject.Find("GlobalState").GetComponent<TrackLikert>();
        }
    }

    /// <summary>
    /// Update Likert when triggering tag, then disable option until reset
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("likert toggle : entered trigger");

        if (col.CompareTag(targetTag)) {
            tracker.Increment(col.name, rtype);
        }
    }

}
