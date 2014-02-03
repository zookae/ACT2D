using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SetSpriteAndText))]
public class RotateSpriteAndTextDB : MonoBehaviour {

    /// <summary>
    /// Tag that causes sprite to change
    /// </summary>
    public string changeTag;

    /// <summary>
    /// Whether rotating is random selection among alternatives (w/repeat)
    /// or strict sequencing
    /// </summary>
    public bool isRandom = false;

    private SetSpriteAndText mySetter;

    void Awake() {
        mySetter = gameObject.GetComponent<SetSpriteAndText>();
    }

    void Start() {
        mySetter.assignSpriteAndTextRandom();
    }


    /// <summary>
    /// Changes sprite on collision with a tag
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D( Collider2D col ) {
        //Debug.Log("destroy : entered trigger");
        if( col.CompareTag(changeTag) ) {
            mySetter.assignSpriteAndTextRandom();
        }

    }
}
