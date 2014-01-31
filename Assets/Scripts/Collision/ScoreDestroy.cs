using UnityEngine;
using System.Collections;

public class ScoreDestroy : MonoBehaviour {

    public int scoreChange = 1;

    void OnDestroy() {
        Debug.Log("[ScoreDestroy] adding score");
        GameState.Singleton.AddScore(scoreChange);
    }
}
