using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreTriggerTagOnce : MonoBehaviour {

    /// <summary>
    /// List of tags that are "good" / give score
    /// </summary>
    public List<string> scoreTag;

    /// <summary>
    /// Points gained for colliding with good tag
    /// </summary>
    public int pointChange;

    /// <summary>
    /// Starting point you must be near to reset ability to score
    /// </summary>
    private Vector3 origin;

    public bool hasScored = false;

    void Start() {
        origin = transform.position;
    }

    void OnTriggerEnter(Collider col) {

        if (GameState.Singleton.CurrentState == State.Running && !hasScored) {
            Debug.Log("score : entered trigger");
            foreach (string t in scoreTag) {
                if (col.CompareTag(t)) {
                    GameState.Singleton.score += pointChange;
                    hasScored = true;
                }
            }
        }
    }

    void Update() {
        if (Mathf.Abs(Vector3.Distance(transform.position, origin)) < 0.1) {
            hasScored = false;
        }
    }
}
