using UnityEngine;
using System.Collections;

public class ShowTimed : MonoBehaviour {

    /// <summary>
    /// Delay until this object becomes visible
    /// </summary>
    public float delay = 1.1f;

    public GameObject targetObject;

    /// <summary>
    /// Track time spent visible
    /// </summary>
    public float hiddenTime = 0.0f;

		
	// Update is called once per frame
	void Update () {
        hiddenTime += Time.deltaTime;

        if (hiddenTime > delay) {
            targetObject.gameObject.SetActive(true);
            hiddenTime = 0.0f;
        }
	}
}
