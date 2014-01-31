using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnSeries : MonoBehaviour {

    ///// <summary>
    ///// Prefab for targets to generate
    ///// </summary>
    //public GameObject target;
    /// <summary>
    /// Tag to assign to new targets
    /// </summary>
    public List<string> tags;
    /// <summary>
    /// Whether to spawn targets that obey gravity or are kinematic
    /// </summary>
    public bool haveGravity = false;
    /// <summary>
    /// Number of objects to spawn in the set
    /// </summary>
    public int numObjects;
    /// <summary>
    /// Maximum distance of any object from spawner center
    /// </summary>
    public float maxDistance;
    /// <summary>
    /// List of spawned objects
    /// </summary>
    private List<GameObject> spawn;

    ///// <summary>
    ///// Target object to move toward
    ///// </summary>
    //public GameObject movementTarget; // TODO : replace with generic script to attach
    ///// <summary>
    ///// Speed for spawned objects to move toward target
    ///// </summary>
    //public float movementRate;

	// Use this for initialization
	void Start () {
        spawn = new List<GameObject>();

        float xmin = this.transform.position.x - maxDistance;
        float xmax = this.transform.position.x + maxDistance;
        float zmin = this.transform.position.z - maxDistance;
        float zmax = this.transform.position.z + maxDistance;

        // 0. compute radius of sphere centered on object to use for overlap detection
        //float targetSize = Vector3.Distance(target.transform.collider.bounds.min, target.transform.collider.bounds.max);
        float targetSize = 10.0f;

        // repeatedly try to generate
        while (spawn.Count < numObjects) {
            // 1. pick point
            Vector3 position = new Vector3(Random.Range(xmin, xmax), 1, Random.Range(zmin, zmax)); // x,z plane

            // 2. ensure not more than maxDistance away
            if (Vector3.Distance(position, this.transform.position) > maxDistance) {
                continue;
            }

            // 3. check if occupied
            if (!Physics.CheckSphere(position, targetSize)) {
                // 4. spawn object there if not
                GameObject newTarget = SpawnTriggerable(); // generate object
                newTarget.transform.position = position; // set position

                //newTarget.gameObject.AddComponent<MoveToTarget>();
                //newTarget.GetComponent<MoveToTarget>().movementTarget = movementTarget;
                //newTarget.GetComponent<MoveToTarget>().moveRate = movementRate * Random.Range(0.75f, 1.25f); // give some randomness to speed

                spawn.Add(newTarget); // add to list of generated
            }
        }
	}
	
	// Update is called once per frame
    //void Update () {
	
    //}

    /// <summary>
    /// Spawns a target configred to fire triggers using a kinematic Rigidbody
    /// </summary>
    /// <returns></returns>
    GameObject SpawnTriggerable() {
        // create the object
        GameObject newTarget = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //GameObject newTarget = (GameObject)GameObject.Instantiate(target, transform.position, transform.rotation);

        newTarget.renderer.material.SetColor("_SpecColor", Color.red);

        // attach a Rigidbody for collisions
        newTarget.gameObject.AddComponent<Rigidbody>();

        // set properties to either obey gravity OR float in space
        if (haveGravity) {
            newTarget.GetComponent<Rigidbody>().useGravity = true;
            newTarget.GetComponent<Rigidbody>().isKinematic = false;
        } else {
            newTarget.GetComponent<Rigidbody>().useGravity = false;
            newTarget.GetComponent<Rigidbody>().isKinematic = true;
        }

        // set trigger property so it can be noticed by OnTriggerEnter
        newTarget.GetComponent<BoxCollider>().isTrigger = true;

        // assign set of tags for other components to use
        foreach (string tag in tags) {
            newTarget.tag = tag;
        }
        return newTarget;
    }
}
