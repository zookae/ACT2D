using UnityEngine;
using System.Collections;

public class ParameterSlider : MonoBehaviour {

    public float xPos;
    public float yPos;
    public float xSize;
    public float ySize;

    public int fontSize;
    public Color fontColor = Color.black;

    public float paramMin;
    public float paramMax;
    public string paramName;

    public float LabelSlider(Rect screenRect, float paramVal, int fontSize, Color fontColor) {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = fontColor;

        Rect labelRect = screenRect;
        labelRect.x += screenRect.width;
        GUI.Label(labelRect, paramName, style);

        // Push the Slider to the end of the Label
        //screenRect.x += screenRect.width;

        paramVal = GUI.HorizontalSlider(screenRect, paramVal, paramMin, paramMax);
        return paramVal;
    }

    /// <summary>
    /// Update a "Shoot" component parameter to a new value.
    /// </summary>
    /// <param name="paramtype">The type of the Shoot parameter to update</param>
    /// <param name="script">Shoot script to update</param>
    /// <param name="newval">Value to update to</param>
    internal void SetParameter(ParamType paramtype, MonoBehaviour script, float newval) {
        switch (paramtype) {
            case ParamType.BULLET_SPEED:
                ((Shoot)script).bulletSpeed = newval;
                break;
            case ParamType.BULLET_SIZE:
                ((Shoot)script).spawn.transform.localScale = new Vector3(newval, newval);
                break;
            case ParamType.FIRERATE:
                ((Shoot)script).gameObject.GetComponent<Shoot>().frequency = newval;
                break;
            case ParamType.MOVE_FORCE:
                ((MoveByKeyForce)script).force = newval;
                break;
            case ParamType.MOVE_DRAG:
                ((MoveByKeyForce)script).drag = newval;
                break;
        }
    }
}
