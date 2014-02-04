using UnityEngine;
using System.Collections;

public class DestroyCameraBound : MonoBehaviour {

    void Update() {
        float dist = (transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        if (transform.position.x < leftBorder ||
            transform.position.x > rightBorder ||
            transform.position.y < bottomBorder ||
            transform.position.y > topBorder) {
                Destroy(gameObject);
        }
    }
}
