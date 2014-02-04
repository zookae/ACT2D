using UnityEngine;
using System.Collections;

public class QuestionReplace : MonoBehaviour {

    private bool isQuitting = false;

    void OnApplicationQuit() {
        isQuitting = true;
    }

    void OnDestroy() {
        if( !isQuitting ) {
            QuestionPool.Singleton.replace(gameObject.GetInstanceID());
        }
    }
}
