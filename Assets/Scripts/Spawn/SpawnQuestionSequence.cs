using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnQuestionSequence : MonoBehaviour {


    public List<GameObject> spawnCollection;

    public GameObject[] activeCollection;

    public int maxActive;
    public int curIdx = 0;

    void Awake() {
        activeCollection = new GameObject[maxActive];
        while (curIdx < maxActive) {
            activeCollection[curIdx] = Spawn();
        }
        curIdx = 0;
    }

	void Update () {
        //Debug.Log("number objects in the queue is " + activeCollection.Count);
        //// if collection is empty add a new object
        //if (activeCollection.Count < 1) {
        //    AddSpawn();
        //}
	}

    GameObject Spawn() {
        int nextSpawn = Random.Range(0, spawnCollection.Count);
        Debug.Log("[SpawnQuestionSequence].AddSpawn : picked to add " + nextSpawn);

        GameObject newSpawn = GameObject.Instantiate(spawnCollection[nextSpawn]) as GameObject;
        newSpawn.SetActive(true);
        newSpawn.transform.parent = transform.parent;
        return(newSpawn);
    }
}


