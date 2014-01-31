using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    /// <summary>
    /// Point to spawned from
    /// </summary>
    public Vector2 spawnPoint;

	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
	}

    void OnDestroy() {
        GameObject newObj = GameObject.Instantiate(gameObject, spawnPoint, Quaternion.identity) as GameObject;
        newObj.transform.parent = transform.parent;
        newObj.SetActive(true);
        foreach (MonoBehaviour c in newObj.GetComponents<MonoBehaviour>()) {
            c.enabled = true;
        }
        newObj.collider2D.enabled = true;
    }
}
