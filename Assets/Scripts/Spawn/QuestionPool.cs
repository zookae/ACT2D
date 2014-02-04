using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(LoadSprites), typeof(LoadText))]
public class QuestionPool : MonoBehaviour {

    #region singleton
    // cf: http://clearcutgames.net/home/?p=437
    // (v1) Allow manipulation in editor and prevent duplicates
    // static singleton property
    public static QuestionPool Singleton { get; private set; }

    // instantiate on game start
    void Awake() {

        // check for conflicting instances
        if( Singleton != null && Singleton != this ) {
            Destroy(gameObject); // destroy others that conflict
        }

        Singleton = this; // save singleton instance

        DontDestroyOnLoad(gameObject); // ensure not destroyed b/t scenes

        questionPrefab = Resources.Load<GameObject>("Prefabs/question destroy");

        spawnedPrefabs = new Dictionary<int, GameObject>();
        
    }
    #endregion

    /// <summary>
    /// Reference object in the layer to spawn objects
    /// </summary>
    public GameObject targetLayer;

    public int spawnNumber = 2; // number of objects to spawn

    private int numSpawned = 0;

    private Dictionary<int, GameObject> spawnedPrefabs;

    private List<GameObject> usedQuestions;

    

    void Start() {
        // store all game objects under their instance IDs + index to ensure no collisions
        for( int i = 0; i < spawnNumber; i++ ) {
            spawned = spawnQuestion();
            spawnedPrefabs[spawned.GetInstanceID()] = spawned;
            numSpawned++;
        }

        foreach( GameObject spawn in spawnedPrefabs.Values ) {
            spawn.transform.position = randomCameraPoint(); // set object position
            spawn.transform.parent = targetLayer.transform.parent; // anchor to target object
        }
    }

    /// <summary>
    /// Prefab with question behavior
    /// </summary>
    public GameObject questionPrefab;

    public GameObject spawned;


    public GameObject spawnQuestion() {
        spawned = GameObject.Instantiate(questionPrefab) as GameObject;

        // assign random sprite and text
        SpriteRenderer spawnSprite = spawned.GetComponent<SpriteRenderer>();
        TextOverlay spawnText = spawned.GetComponent<TextOverlay>();

        if (spawnSprite != null) {
            Debug.Log("[QuestionPool].spawnSprite() changing from sprite " + spawnSprite.sprite);
            spawnSprite.sprite = LoadSprites.Singleton.getSpriteRandom();
        }

        if( spawnText != null ) {
            spawnText.guiText = LoadText.Singleton.getTextRandom();
        }
        return spawned;
    }


    


    /// <summary>
    /// Replace the given prefab by adding a new one
    /// </summary>
    /// <param name="destroyedID"></param>
    public void replace( int destroyedID ) {
        Singleton.spawnedPrefabs.Remove(destroyedID); // remove old object

        GameObject replacement;
        replacement = spawnQuestion();

        // TODO ensure not overlapping question

        replacement.transform.position = randomCameraPoint();
        replacement.transform.parent = targetLayer.transform.parent;

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
        Debug.Log("[QuestionPool].randomCameraPoint() made point " + temp);

        return new Vector3(xPoint, yPoint, targetLayer.transform.position.z);
    }


}