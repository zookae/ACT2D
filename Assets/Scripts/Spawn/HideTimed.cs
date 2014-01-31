using UnityEngine;
using System.Collections;

public class HideTimed : MonoBehaviour {

    /// <summary>
    /// Duration this object remains visible before disappearing
    /// </summary>
    public float duration = 1.0f;

    /// <summary>
    /// Track time spent visible
    /// </summary>
    public float visibleTime = 0.0f;

		
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf) {
            visibleTime += Time.deltaTime;
        }

        if (visibleTime > duration) {
            gameObject.SetActive(false);
            visibleTime = 0.0f;
        }
	}
}
