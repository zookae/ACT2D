using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LikertScale {
    v1,
    v2,
    v3,
    v4,
    v5,
    v6,
    v7
}

public class TrackLikert : MonoBehaviour {

    public Dictionary<string, Dictionary<LikertScale, int>> LikertCount = new Dictionary<string, Dictionary<LikertScale, int>>();

    public void Increment(string question, LikertScale likert) {
        if (LikertCount.ContainsKey(question)) {
            Dictionary<LikertScale, int> counts = LikertCount[question];
            if (counts.ContainsKey(likert)) {
                Debug.Log("[TrackLikert] incrementing existing Likert");
                counts[likert] = counts[likert] + 1;
            }
            else {
                Debug.Log("[TrackLikert] adding new Likert");
                counts[likert] = 1;
            }
        }
        else {
            Debug.Log("[TrackLikert] adding new question + Likert");
            LikertCount.Add(question, new Dictionary<LikertScale, int>());
            LikertCount[question][likert] = 1;
        }
    }
}
