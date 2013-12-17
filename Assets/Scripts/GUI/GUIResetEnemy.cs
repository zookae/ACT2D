using UnityEngine;
using System.Collections;

public class GUIResetEnemy : MonoBehaviour {

    /// <summary>
    /// locations to spawn entities
    /// </summary>
    public Vector2[] entityPositions;

    /// <summary>
    /// prefab of entity being reset
    /// </summary>
    public Transform entityPrefab;

    public const int buttonWidth = 100;
    public const int buttonHeight = 50;

    void OnGUI() {
        if (GUI.Button(new Rect(0, 
            (Screen.height*4/5) - buttonHeight/2, 
            buttonWidth, 
            buttonHeight), "reset enemies")) {
            DestroyOfFaction(Faction.ENEMY); // remove enemies
            SetEntities(entityPositions, entityPrefab); // position them

            // NOTE: this is horrible:
            //  unity can only broadcast/send messages to children of a gameobject
            //  so we make the general scripts for the level share a parent
            //  then have the parent send the message to share...
            transform.parent.BroadcastMessage("LoadComponents"); 
        }
    }

    void Start() {
        // initialize entitiy positions
        SetEntities(entityPositions, entityPrefab);
    }

    /// <summary>
    /// spawn a given prefab at set of 2D positions
    /// </summary>
    /// <param name="positions"></param>
    /// <param name="prefab"></param>
    void SetEntities(Vector2[] positions, Transform prefab) {
        foreach (Vector2 pos in positions) {
            Transform newEntity = GameObject.Instantiate(prefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity) as Transform;
            newEntity.parent = this.transform;
        }
    }

    /// <summary>
    /// destroy all GameObjects of a given Faction
    /// </summary>
    /// <param name="destroyFaction"></param>
    void DestroyOfFaction(Faction destroyFaction) {
        Transform[] objs = GameObject.FindObjectsOfType<Transform>();

        foreach (Transform obj in objs) {
            FactionState faction = obj.GetComponent<FactionState>();
            if (faction != null && faction.faction == destroyFaction) {
                Destroy(obj.gameObject);
            }
        }
    }
}
