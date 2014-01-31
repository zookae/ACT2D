using UnityEngine;
using System.Collections;

public class FloatAndFadeText : MonoBehaviour {

    public string text;
    public float lifetime;
    public Color fontColor;

    public Vector3 velocity = new Vector3(0, 1.0f, 0);
    private float currentTime = 0.0f;
    private GUIStyle style;

	// Use this for initialization
	void Start () {
        // STOPS SOME DEFAULT ALPHA DUMB
        fontColor = (fontColor.a == 0) ? Color.black : fontColor;
	}

    void OnGUI() {
        if (style == null) {
            style = new GUIStyle(GUI.skin.label);
            style.font = Resources.Load("gwap_fonts/Nunito-Regular") as Font;
            style.fontSize = 10;
            style.normal.textColor = fontColor;
        }

        GUI.depth = (int)GUIDepthLevels.GAME_DYNAMIC;
        Vector3 position = this.gameObject.transform.position + ((currentTime / lifetime) * velocity);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        GUILayout.BeginArea(new Rect(screenPos.x, Screen.height - screenPos.y, text.Length * style.fontSize, style.fontSize * 10));
        GUILayout.Label(text, style);
        GUILayout.EndArea();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentTime >= lifetime) {
            GameObject.Destroy(this.gameObject);
            return;
        }
        currentTime += Time.deltaTime;
	}
}
