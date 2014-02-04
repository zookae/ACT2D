using UnityEngine;
using System.Collections;

public class QuestionReplaceFire : MonoBehaviour {

    private bool isQuitting = false;

    void OnApplicationQuit() {
        isQuitting = true;
    }

    void OnDestroy() {
        if( !isQuitting ) {
            GameObject replacement = QuestionPool.Singleton.replace(gameObject.GetInstanceID());
            replacement.GetComponent<MoveTowardTarget>().target = this.gameObject.GetComponent<MoveTowardTarget>().target;
        }
    }
}
