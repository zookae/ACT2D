using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: generalize so not specific to shooting behavior

public class ParameterSliderSelect : ParameterSlider {

    public List<MonoBehaviour> paramArray = new List<MonoBehaviour>();

    public string entity = "Enemy";

    private float newValue;
    private float prevTime = 0.0f;
    private float timeDelta = 0.1f;
    public ParamType ptype;

    /// <summary>
    /// Adds components to be manipulated. 
    /// Needs to be in Start so every object has been added to scene already.
    /// </summary>
    void Start() {
        newValue = (paramMax - paramMin) / 2;
        //Debug.Log("[ParameterSliderSelect] initializing value: " + newValue);

        GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        Debug.Log("[ParameterSliderSelect] objects found: ");
        foreach (GameObject o in objs) {
//            Debug.Log("[ParameterSliderSelect] object " + o.name);
//            Debug.Log("[ParameterSliderSelect] parameter type " + ptype);
            if (ptype == ParamType.BULLET_SIZE ||
                ptype == ParamType.BULLET_SPEED ||
                ptype == ParamType.FIRERATE) {
                    paramArray.AddRange(o.GetComponents<Shoot>());
            } else if (
                ptype == ParamType.MOVE_DRAG ||
                ptype == ParamType.MOVE_FORCE) {
                    paramArray.AddRange(o.GetComponents<MoveByKeyForce>());
            }
        }

        ParamChange pch = new ParamChange(
                GameState.Singleton.TimeUsed,
                ptype,
                entity,
                newValue);
        GameState.Singleton.actionTrace.Add(pch);
    }

    
    void OnGUI() {
        float oldValue = newValue;
        newValue = LabelSlider(new Rect(xPos, yPos, xSize, ySize), newValue, fontSize, fontColor);

        // store action trace
        if (newValue != oldValue &&
            GameState.Singleton.TimeUsed - prevTime > timeDelta) {
            ParamChange pch = new ParamChange(
                GameState.Singleton.TimeUsed,
                ptype,
                entity,
                newValue);
            Debug.Log(pch.ToString());
            GameState.Singleton.actionTrace.Add(pch);

            prevTime = GameState.Singleton.TimeUsed;
        }

        foreach (MonoBehaviour p in paramArray) {
            if (p == null)
                continue;
            SetParameter(ptype, p, newValue);
        }
    }

}

