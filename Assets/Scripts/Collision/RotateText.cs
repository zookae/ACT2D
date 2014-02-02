using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextOverlay))]
public class RotateText : MonoBehaviour {

    /// <summary>
    /// Tag that causes sprite to change
    /// </summary>
    public string changeTag;

    /// <summary>
    /// Whether rotating is random selection among alternatives (w/repeat)
    /// or strict sequencing
    /// </summary>
    public bool isRandom = false;

    private TextOverlay myText;

    /// <summary>
    /// Text to change into
    /// </summary>
    public string[] textCollection;

    private int spriteIdx = 0;

    void Start() {

        myText = gameObject.GetComponent<TextOverlay>();
        myText.guiText = textCollection[spriteIdx];

        if( textCollection.Length > 1 ) {
            spriteIdx++;
        }
    }

    void ChangeText(bool isRandom) {

        myText.guiText = textCollection[spriteIdx];

        if( isRandom ) {
            spriteIdx = Random.Range(0, textCollection.Length);
        }
        else {
            spriteIdx++;
        }

        if( spriteIdx >= textCollection.Length ) {
            spriteIdx = 0;
        }
            

    }

    /// <summary>
    /// Changes sprite on collision with a tag
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D( Collider2D col ) {
        //Debug.Log("destroy : entered trigger");
        if( col.CompareTag(changeTag) ) {
            ChangeText(isRandom);
        }

    }
}
