using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LikertPool : MonoBehaviour {

    #region singleton
    // cf: http://clearcutgames.net/home/?p=437
    // (v1) Allow manipulation in editor and prevent duplicates
    // static singleton property
    public static LikertPool Singleton { get; private set; }

    // instantiate on game start
    void Awake() {

        // check for conflicting instances
        if( Singleton != null && Singleton != this ) {
            Destroy(gameObject); // destroy others that conflict
        }

        Singleton = this; // save singleton instance

        DontDestroyOnLoad(gameObject); // ensure not destroyed b/t scenes

        // load prefab set
        binPrefabs = Resources.LoadAll<GameObject>("Prefabs/likert scale/bin");
        collectPrefabs = Resources.LoadAll<GameObject>("Prefabs/likert scale/collectable");

        // store all game objects under their instance IDs + index to ensure no collisions
        spawnedPrefabs = new Dictionary<int, GameObject>();
        for( int i = 0; i < spawnNumber; i++ ) {
            GameObject spawned;
            if( isBin ) {
                spawned = spawnBin();
            }
            else {
                spawned = spawnCollect();
            }
            spawnedPrefabs[spawned.GetInstanceID()] = spawned;
            numSpawned++;
        }
    }
    #endregion

    public GameObject[] binPrefabs;
    public GameObject[] collectPrefabs;

    public GameObject targetLayer;

    public int spawnNumber = 2;

    private int numSpawned = 0;

    void Start() {
        foreach( GameObject spawn in spawnedPrefabs.Values ) {
            spawn.transform.position = randomCameraPoint(); // set object position

            spawn.transform.parent = targetLayer.transform.parent; // anchor to target object
        }
    }

    /// <summary>
    /// Whether spawning bins that questions are put in 
    ///     or collectables for questions to gather
    /// </summary>
    public bool isBin = false;

    public Dictionary<int, GameObject> spawnedPrefabs;

    /// <summary>
    /// Spawn a random bin prefab
    /// </summary>
    /// <returns></returns>
    public GameObject spawnBin() {
        int prefabIdx = Random.Range(0, binPrefabs.Length - 1);

        GameObject spawned = GameObject.Instantiate(binPrefabs[prefabIdx]) as GameObject;
        Debug.Log("[LikertPool].spawnBin() adding to pool: " + spawned.name + " : " + spawned.GetInstanceID());
        return spawned;
    }

    /// <summary>
    /// Spawn a random collectable prefab
    /// </summary>
    /// <returns></returns>
    public GameObject spawnCollect() {
        int prefabIdx = Random.Range(0, collectPrefabs.Length - 1);

        GameObject spawned = GameObject.Instantiate(collectPrefabs[prefabIdx]) as GameObject;
        Debug.Log("[LikertPool].spawnCollect() adding to pool: " + spawned.name + " : " + spawned.GetInstanceID());
        return spawned;
    }

    /// <summary>
    /// Replace the given prefab by adding a new one
    ///     note: Does not replace the slot, but just number
    /// </summary>
    /// <param name="destroyedID"></param>
    public void replace( int destroyedID ) {
        Singleton.spawnedPrefabs.Remove(destroyedID); // remove old object

        GameObject replacement;
        if( isBin ) {
            replacement = spawnBin();
        }
        else {
            replacement = spawnCollect();
        }
        replacement.transform.position = randomCameraPoint();

        replacement.transform.parent = targetLayer.transform.parent;

        Singleton.spawnedPrefabs[replacement.GetInstanceID()] = replacement;
        numSpawned++;
    }
    
    // ability to set spawn object position w/in camera w/o overlap
    public Vector3 randomCameraPoint() {
        float dist = (targetLayer.transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        float xPoint = Random.Range(leftBorder, rightBorder);
        float yPoint = Random.Range(bottomBorder, topBorder);

        Vector3 temp = new Vector3(xPoint, yPoint, targetLayer.transform.position.z);
        Debug.Log("[LikertPool].randomCameraPoint() made point " + temp);

        return new Vector3(xPoint, yPoint, targetLayer.transform.position.z);
    }

}