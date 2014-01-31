using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Spawn : MonoBehaviour {

    /// <summary>
    /// Prefab for targets to generate
    /// </summary>
    public GameObject spawn;
    /// <summary>
    /// Tag to assign to new targets
    /// </summary>
    public string assignTag;
    /// <summary>
    /// Whether to spawn targets that obey gravity or are kinematic
    /// </summary>
    public bool haveGravity = false;


    /// <summary>
    /// Spawns a target configred to fire triggers using a kinematic Rigidbody
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnTriggerable() {
        //Debug.Log("[SpawnTriggerable]");
        // create the object
        GameObject newTarget = (GameObject)GameObject.Instantiate(spawn, transform.position, transform.rotation);

        // attach a Rigidbody for collisions
        if (newTarget.gameObject.GetComponent<Rigidbody>() == null) {
            newTarget.gameObject.AddComponent<Rigidbody>();
        }

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
        newTarget.tag = assignTag;
        
        return newTarget;
    }



}
