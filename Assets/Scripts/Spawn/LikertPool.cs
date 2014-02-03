using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

        binPrefabsLikert = new Dictionary<LikertScale, GameObject>();
        foreach( GameObject prefab in binPrefabs ) {
            binPrefabsLikert[prefab.gameObject.GetComponent<LikertChangeTag>().rtype] = prefab;
        }

        collectPrefabs = Resources.LoadAll<GameObject>("Prefabs/likert scale/collectable");

        collectPrefabsLikert = new Dictionary<LikertScale, GameObject>();
        foreach( GameObject prefab in collectPrefabs ) {
            collectPrefabsLikert[prefab.gameObject.GetComponent<LikertChangeTag>().rtype] = prefab;
        }


        GameObject spawned;
        spawnedPrefabs = new Dictionary<int, GameObject>();
        
        if( spawnNumber < likertNumber ) {
            Debug.LogError("[LikertPool] spawning too few objects to ensure all categories are covered!");
        }

        // initialize likert counting
        likertCount = new Dictionary<LikertScale, int>();
        foreach( LikertScale ls in Enum.GetValues(typeof(LikertScale)) ) {
            if( ls != LikertScale.empty ) {
                likertCount[ls] = 0;

                if( isBin ) {
                    spawned = spawnBin(ls);
                }
                else {
                    spawned = spawnCollect(ls);
                }
                spawnedPrefabs[spawned.GetInstanceID()] = spawned;
                numSpawned++;
            }
        }


        // store all game objects under their instance IDs + index to ensure no collisions
        for( int i = likertNumber; i < spawnNumber; i++ ) {
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

    public Dictionary<LikertScale, GameObject> binPrefabsLikert;
    public Dictionary<LikertScale, GameObject> collectPrefabsLikert;

    /// <summary>
    /// Reference object in the layer to spawn objects
    /// </summary>
    public GameObject targetLayer;

    public int spawnNumber = 2; // number of objects to spawn

    public const int likertNumber = 5; // number of items on likert scale

    private int numSpawned = 0;

    /// <summary>
    /// Whether spawning bins that questions are put in 
    ///     or collectables for questions to gather
    /// </summary>
    public bool isBin = false;

    private Dictionary<int, GameObject> spawnedPrefabs;

    private Dictionary<LikertScale, int> likertCount;

    public void likertIncrement( LikertScale likert ) {
        if( likertCount.ContainsKey(likert) ) {
            likertCount[likert] = likertCount[likert] + 1;
        }
        else {
            likertCount[likert] = 1;
        }
        Debug.Log("[LikertPool].likertIncrement() " + likert + " @ " + likertCount[likert]);
    }

    public void likertDecrement( LikertScale likert ) {
        if( likertCount.ContainsKey(likert) ) {
            likertCount[likert] = likertCount[likert] - 1;
        }
        else {
            Debug.LogError("[LikertPool].likertDecrement() lowering count of non-existent key");
            likertCount[likert] = 0;
        }
        Debug.Log("[LikertPool].likertDecrement() " + likert + " @ " + likertCount[likert]);
    }


    void Start() {
        foreach( GameObject spawn in spawnedPrefabs.Values ) {
            spawn.transform.position = randomCameraPoint(); // set object position
            spawn.transform.parent = targetLayer.transform.parent; // anchor to target object

            LikertChangeTag spawnLik = spawn.gameObject.GetComponent<LikertChangeTag>();
            if( spawnLik == null ) {
                Debug.LogError("spawning non-likert object!");
            }
            else {
                likertIncrement(spawnLik.rtype);
            }
        }
    }




    /// <summary>
    /// Spawn a random bin prefab
    /// </summary>
    /// <returns></returns>
    public GameObject spawnBin() {
        int prefabIdx = UnityEngine.Random.Range(0, binPrefabs.Length - 1);

        GameObject spawned = GameObject.Instantiate(binPrefabs[prefabIdx]) as GameObject;
        Debug.Log("[LikertPool].spawnBin() adding to pool: " + spawned.name + " : " + spawned.GetInstanceID());
        return spawned;
    }

    /// <summary>
    /// Spawn a random bin prefab
    /// </summary>
    /// <returns></returns>
    public GameObject spawnBin( LikertScale likertS ) {
        GameObject spawned = GameObject.Instantiate(binPrefabsLikert[likertS]) as GameObject;
        Debug.Log("[LikertPool].spawnBin(LikertScale) adding to pool: " + spawned.name + " : " + spawned.GetInstanceID());
        return spawned;
    }

    /// <summary>
    /// Spawn a random collectable prefab
    /// </summary>
    /// <returns></returns>
    public GameObject spawnCollect() {
        int prefabIdx = UnityEngine.Random.Range(0, collectPrefabs.Length - 1);

        GameObject spawned = GameObject.Instantiate(collectPrefabs[prefabIdx]) as GameObject;
        Debug.Log("[LikertPool].spawnCollect() adding to pool: " + spawned.name + " : " + spawned.GetInstanceID());
        return spawned;
    }

    /// <summary>
    /// Spawn a random collectable prefab
    /// </summary>
    /// <returns></returns>
    public GameObject spawnCollect( LikertScale likertS ) {
        GameObject spawned = GameObject.Instantiate(collectPrefabsLikert[likertS]) as GameObject;
        Debug.Log("[LikertPool].spawnCollect(LikertScale) adding to pool: " + spawned.name + " : " + spawned.GetInstanceID());
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
        if( allLikertUsed() ) {
            if( isBin ) {
                replacement = spawnBin();
            }
            else {
                replacement = spawnCollect();
            }
        }
        else {
            LikertScale badLik = likertUnused();
            if( isBin ) {
                replacement = spawnBin(badLik);
            }
            else {
                replacement = spawnCollect(badLik);
            }
        }

        replacement.transform.position = randomCameraPoint();
        replacement.transform.parent = targetLayer.transform.parent;

        LikertChangeTag spawnLik = replacement.gameObject.GetComponent<LikertChangeTag>();
        if( spawnLik == null ) {
            Debug.LogError("spawning non-likert object!");
        }
        else {
            likertIncrement(spawnLik.rtype);
        }

        Singleton.spawnedPrefabs[replacement.GetInstanceID()] = replacement;
        numSpawned++;
    }


    // ability to set spawn object position w/in camera w/o overlap
    /// <summary>
    /// Pick a random point that is currently on Camera
    /// </summary>
    /// <returns></returns>
    public Vector3 randomCameraPoint() {
        float dist = (targetLayer.transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        float xPoint = UnityEngine.Random.Range(leftBorder, rightBorder);
        float yPoint = UnityEngine.Random.Range(bottomBorder, topBorder);

        Vector3 temp = new Vector3(xPoint, yPoint, targetLayer.transform.position.z);
        Debug.Log("[LikertPool].randomCameraPoint() made point " + temp);

        return new Vector3(xPoint, yPoint, targetLayer.transform.position.z);
    }

    /// <summary>
    /// Are all Likert values being used with at least one question?
    /// </summary>
    /// <returns></returns>
    private bool allLikertUsed() {
        bool allUsed = true;
        foreach( int likCt in likertCount.Values ) {
            if( likCt < 1 ) {
                allUsed = false;
            }
        }
        return allUsed;
    }

    /// <summary>
    /// Return the first Likert scale item with less than one questions
    /// </summary>
    /// <returns></returns>
    private LikertScale likertUnused() {
        foreach( LikertScale likS in likertCount.Keys ) {
            if( likertCount[likS] < 1 ) {
                return likS;
            }
        }
        return LikertScale.empty;
    }

}