﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class RotateSprite : MonoBehaviour {

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

    /// <summary>
    /// Sprite to change into
    /// </summary>
    public Sprite[] spriteCollection;

    public int spriteIdx = 0;

    void Start() {

        if( isRandom ) {
            spriteIdx = Random.Range(0, spriteCollection.Length - 1);
        }

        mySprite = gameObject.GetComponent<SpriteRenderer>();
        mySprite.sprite = spriteCollection[spriteIdx];

        if( spriteCollection.Length > 1 ) {
            spriteIdx++;
        }
    }

    void ChangeSprite(bool isRandom) {

        mySprite.sprite = spriteCollection[spriteIdx];

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
            ChangeSprite(isRandom);
        }

    }
}
