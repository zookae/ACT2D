using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LikertChangeTag))]
public class LikertReplace : MonoBehaviour {

    private bool isQuitting = false;

    void OnApplicationQuit() {
        isQuitting = true;
    }

    void OnDestroy() {
        if( !isQuitting ) {
            LikertPool.Singleton.likertDecrement(gameObject.GetComponent<LikertChangeTag>().rtype);

            LikertPool.Singleton.replace(gameObject.GetInstanceID());
        }
    }
}
