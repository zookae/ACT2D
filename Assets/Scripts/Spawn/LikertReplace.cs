using UnityEngine;
using System.Collections;

public class LikertReplace : MonoBehaviour {

    void OnDestroy() {
        LikertPool.Singleton.replace(gameObject.GetInstanceID());
    }
}
