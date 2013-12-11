using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;



public class ParameterToggle : MonoBehaviour {

    public int selGridInt = 0;
    public ComponentBehavior[] selBehaviors;
    public string[] selStrings;


    public string entity = "Player";
    private ParamType ptype;

    public List<MonoBehaviour> paramArray = new List<MonoBehaviour>();
    public List<GameObject> targetObjects;

    void Start() {
        selStrings = new string[selBehaviors.Length];
        for (int i = 0; i < selBehaviors.Length; i++) {
            selStrings[i] = selBehaviors[i].ToString();
        }

        GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        targetObjects.AddRange(GameObject.FindGameObjectsWithTag(entity));
    }

    void OnGUI() {
        selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 2);

        switch (selBehaviors[selGridInt]) {
            case ShootBehavior.SHOOT_DIR_DOWN:
                setComponents("isShootInDirectionDown");
                break;
            case ShootBehavior.SHOOT_TOWARD:
                setComponents("isShootToward");
                break;
            default:
                break;
        }
        
        
    }


    void setComponents(string behaviorName) {
        MethodInfo mi = typeof(ParameterToggle).GetMethod(behaviorName);
        //Debug.Log("[ParameterToggle].setComponents method is " + mi.ToString());
        foreach (GameObject g in targetObjects) {
            if (g == null) {
                //targetObjects.Remove(g);
				continue;
            }
            //Debug.Log("[ParameterToggle].setComponents gameobject is " + g.name);
            foreach (Shoot s in g.GetComponents<Shoot>()) {
                //Debug.Log("[ParameterToggle].setComponents component is " + s.name);
                bool testResult = (bool)mi.Invoke(this, new object[] { s });
                if (testResult) {
                    s.enabled = true;
                }
                else {
                    s.enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Test whether a shoot behavior specifies to shoot directly down
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool isShootInDirectionDown(Shoot s) {
        if (s is INPCShootBehavior &&
            s is NPCShootInDirection) {
            NPCShootInDirection s2 = (NPCShootInDirection)s;

            if (s2.moveDir == MoveDirection.Down) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Test whether a shoot behavior specifies to shoot toward a target
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool isShootToward(Shoot s) {
        if (s is INPCShootBehavior &&
            s is NPCShootTowardTarget) {
            return true;
        }
        return false;
    }
}
