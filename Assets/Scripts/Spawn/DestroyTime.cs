using UnityEngine;
using System.Collections;

/// <summary>
/// automatically destroy object after a period of time
/// </summary>
public class DestroyTime : MonoBehaviour {

    /// <summary>
    /// duration object persists
    /// </summary>
    public float lifetime = 10f;

	void Start() {
        Destroy(gameObject, lifetime);
	}
}
