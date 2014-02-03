using UnityEngine;
using System.Collections;

public class LikertReplace : MonoBehaviour {

    private bool isQuitting = false;

    void OnApplicationQuit() {
        isQuitting = true;
    }

    void OnDestroy() {
        if( !isQuitting ) {
            LikertPool.Singleton.replace(gameObject.GetInstanceID());
        }
    }
}
