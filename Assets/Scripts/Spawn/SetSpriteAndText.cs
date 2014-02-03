using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(TextOverlay))]
public class SetSpriteAndText : MonoBehaviour {

    private SpriteRenderer mySprite;
    private TextOverlay myText;

    void Awake() {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        myText = gameObject.GetComponent<TextOverlay>();
    }

    //private float timeLapse = 0f;
    //void Update() {
    //    timeLapse += Time.deltaTime;
    //    if (timeLapse > 3f) {
    //        assignSpriteAndTextRandom();
    //        timeLapse = 0f;
    //    }
    //}

    public void assignSpriteAndTextRandom() {
        myText.guiText = LoadText.Singleton.getTextRandom();
        mySprite.sprite = LoadSprites.Singleton.getSpriteRandom();
    }

}
