using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// components exposed for manipulation
/// </summary>
public enum ComponentBehavior {
    MOVE_KEY_TRANSLATE,
    MOVE_KEY_FORCE
}


public class ComponentAdd : MonoBehaviour {

    public int selGridInt = 0;
    public ComponentBehavior[] selBehaviors;
    public string[] selStrings;


    public string entity = "Player";

    public List<GameObject> targetObjects;

	
	void Start() {
        selStrings = new string[selBehaviors.Length];
        for (int i = 0; i < selBehaviors.Length; i++) {
            selStrings[i] = selBehaviors[i].ToString();
        }

        //GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        targetObjects.AddRange(GameObject.FindGameObjectsWithTag(entity));
	}

    void OnGUI() {
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 2);

        switch (selBehaviors[selGridInt]) {
            case ComponentBehavior.MOVE_KEY_TRANSLATE:
                break;
            case ComponentBehavior.MOVE_KEY_FORCE:
                break;
            default:
                break;
        }
    }
}
