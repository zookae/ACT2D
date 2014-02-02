using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(TextOverlay))]
public class RotateSpriteAndText : MonoBehaviour {

    /// <summary>
    /// Tag that causes sprite to change
    /// </summary>
    public string changeTag;

    /// <summary>
    /// Whether rotating is random selection among alternatives (w/repeat)
    /// or strict sequencing
    /// </summary>
    public bool isRandom = false;

    private SpriteRenderer mySprite;
    private TextOverlay myText;

    /// <summary>
    /// Sprite to change into
    /// </summary>
    public Sprite[] spriteCollection;

    /// <summary>
    /// Text to change into
    /// </summary>
    public string[] textCollection;

    public int spriteIdx = 0;

    void Start() {

        if( spriteCollection.Length != textCollection.Length ) {
            Debug.LogError("two collections not equal length!");
        }

        if( isRandom ) {
            spriteIdx = Random.Range(0, spriteCollection.Length - 1);
        }

        mySprite = gameObject.GetComponent<SpriteRenderer>();
        mySprite.sprite = spriteCollection[spriteIdx];

        myText = gameObject.GetComponent<TextOverlay>();
        myText.guiText = textCollection[spriteIdx];

        if( spriteCollection.Length > 1 ) {
            spriteIdx++;
        }
    }

    void ChangeSpriteAndText( bool isRandom ) {

        mySprite.sprite = spriteCollection[spriteIdx];
        myText.guiText = textCollection[spriteIdx];

        if( isRandom ) {
            spriteIdx = Random.Range(0, spriteCollection.Length-1);
        }
        else {
            spriteIdx++;
        }

        if( spriteIdx >= spriteCollection.Length ) {
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
            ChangeSpriteAndText(isRandom);
        }

    }
}
