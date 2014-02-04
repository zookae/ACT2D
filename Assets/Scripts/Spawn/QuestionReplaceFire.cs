using UnityEngine;
using System.Collections;

public class QuestionReplaceFire : MonoBehaviour {

    private bool isQuitting = false;

    public Transform spawnBounds;

    void OnApplicationQuit() {
        isQuitting = true;
    }

    void OnDestroy() {
        if( !isQuitting ) {
            GameObject replacement = QuestionPool.Singleton.replace(gameObject.GetInstanceID(), spawnBounds);
            replacement.GetComponent<MoveTowardTarget>().target = this.gameObject.GetComponent<MoveTowardTarget>().target;
            replacement.GetComponent<QuestionReplaceFire>().spawnBounds = this.gameObject.GetComponent<QuestionReplaceFire>().spawnBounds;
        }
    }
}
