using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: generalize so not specific to shooting behavior

public class ParameterSliderSelect : ParameterSlider {

    public List<MonoBehaviour> paramArray = new List<MonoBehaviour>();

    public string entity = "Enemy";

    public float newValue;
    private float prevTime = 0.0f;
    private float timeDelta = 0.1f;
    public ParamType ptype;

    /// <summary>
    /// Adds components to be manipulated. 
    /// Needs to be in Start so every object has been added to scene already.
    /// </summary>
    void Start() {

        LoadComponents();
        //ParamChange pch = new ParamChange(
        //        GameState.Singleton.TimeUsed,
        //        ptype,
        //        entity,
        //        newValue);
        //GameState.Singleton.actionTrace.Add(pch);
    }

    void LoadComponents() {
GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        Debug.Log("[ParameterSliderSelect] - LoadComponents - objects found: ");
        foreach (GameObject o in objs) {
            if (ptype == ParamType.BULLET_SIZE ||
                ptype == ParamType.BULLET_SPEED ||
                ptype == ParamType.FIRERATE) {
                paramArray.AddRange(o.GetComponents<Shoot>());
            }
        }
    }

    
    void OnGUI() {
        float oldValue = newValue;
        newValue = LabelSlider(new Rect(xPos, yPos, xSize, ySize), newValue, fontSize, fontColor);

        // store action trace
        if (newValue != oldValue &&
            Time.timeSinceLevelLoad - prevTime > timeDelta) {
            //ParamChange pch = new ParamChange(
            //    GameState.Singleton.TimeUsed,
            //    ptype,
            //    entity,
            //    newValue);
            //Debug.Log(pch.ToString());
            //GameState.Singleton.actionTrace.Add(pch);

            Debug.Log("[ParameterSliderSelect] new parameter value: " + ptype + " - " + entity + " : " + newValue);
            prevTime = Time.timeSinceLevelLoad;
        }

        foreach (MonoBehaviour p in paramArray) {
            if (p == null)
                continue;
            SetParameter(ptype, p, newValue);
        }
    }

}

