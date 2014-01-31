using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public float barDisplay;

    public Vector2 size;

    public Color progressBarEmpty;
    public Color progressBarFull;

    public float maxtime;

    private Vector2 position;
    private Texture2D emptyStyle;
    private Texture2D fullStyle;

    void Start() {
        size = (size.x == 0 || size.y == 0) ? new Vector2(100, 20) : size;
        //position = (position == null) ? new Vector2(, Screen.height - 100) : position;
        position = new Vector2((Screen.width - size.x) / 2, Screen.height - ((3 * size.y) / 2));

        // Note, the 2x makes it big enough, I've found.
        emptyStyle = GUIUtils.MakeBlankTexture((int)size.x * 2, (int)size.y, progressBarEmpty);
        fullStyle = GUIUtils.MakeBlankTexture((int)size.x * 2, (int)size.y, progressBarFull);
    }

    void OnGUI() {
        //if (GameState.Singleton.CurrentState == State.Running) {
            GUI.depth = (int)GUIDepthLevels.DISPLAY_ELEMENT;
            GUI.BeginGroup(new Rect(position.x, position.y, size.x, size.y));
            GUI.Box(new Rect(0, 0, size.x, size.y), emptyStyle);
            // draw the filled-in part:
            GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
            GUI.Box(new Rect(0, 0, size.x, size.y), fullStyle);
            GUI.EndGroup();
            GUI.EndGroup();
        //}
    }

    void Update() {
        barDisplay = Time.timeSinceLevelLoad / maxtime;
        //if (GameState.Singleton.CurrentState == State.Running) {
        //    barDisplay = GameState.Singleton.TimeUsed / this.maxtime;
        //}
    }
    
}