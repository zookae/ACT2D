using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SetSprite : MonoBehaviour {

    /// <summary>
    /// Sprite to set object to
    /// </summary>
    public string targetSprite;

    private SpriteRenderer mySprite;

    void Awake() {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void assignSprite(string spriteName) {
        mySprite.sprite = LoadSprites.Singleton.getSprite(spriteName);
    }

    public void assignSpriteRandom() {
        mySprite.sprite = LoadSprites.Singleton.getSpriteRandom();
    }
	
}
