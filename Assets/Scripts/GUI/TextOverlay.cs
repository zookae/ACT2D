using UnityEngine;
using System.Collections;

public class TextOverlay : MonoBehaviour {

    /// <summary>
    /// Overlay text
    /// </summary>
    public string guiText;

    /// <summary>
    /// Size of overlay text
    /// </summary>
    public int useFontSize;

    /// <summary>
    /// Color of background behind overlay text
    /// </summary>
    public Color backgroundColor;

    private Vector2 textSize;
    private Vector2 padding = new Vector2(1, 1); // padding around text

    public int guiDepth = 1;


    private GUIStyle textStyle;
    private GUIStyle boxStyle;
    private Vector3 pixelPosition;
    private Vector2 p;

    void Awake() {

        textStyle = new GUIStyle();
        textStyle.font = Resources.Load("fonts/Nunito-Regular") as Font;
        textStyle.fontSize = useFontSize;
        textStyle.alignment = TextAnchor.MiddleCenter;
        textStyle.normal.textColor = Color.black;

        

        //boxStyle = new GUIStyle(GUI.skin.box);
        //boxStyle.normal.background = GUIUtils.MakeBlankTexture((int)textSize.x * 2, (int)textSize.y * 2, backgroundColor);

        GUI.depth = guiDepth;

    }

    void OnGUI() {
        textSize = textStyle.CalcSize(new GUIContent(guiText)) + padding;
        if (boxStyle == null) {
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = GUIUtils.MakeBlankTexture((int)textSize.x * 2, (int)textSize.y * 2, backgroundColor);
            GUI.depth = guiDepth;
        }
        
        pixelPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        p = new Vector2(pixelPosition.x, Screen.height - pixelPosition.y);
        GUILayout.BeginArea(new Rect(p.x - (textSize.x / 2), p.y - (textSize.y / 2), textSize.x, textSize.y), boxStyle);
        GUILayout.Label(guiText, textStyle);
        GUILayout.EndArea();
    }

}
