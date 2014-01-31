using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeSprite : MonoBehaviour {

    /// <summary>
    /// Tag that causes sprite to change
    /// </summary>
    public string changeTag;

    private SpriteRenderer mySprite;

    /// <summary>
    /// Sprite to change into
    /// </summary>
    public Sprite newSprite;

    void Start() {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Changes sprite on collision with a tag
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D( Collider2D col ) {
        //Debug.Log("destroy : entered trigger");
        if( col.CompareTag(changeTag) ) {
            mySprite.sprite = newSprite;
        }

    }
}
