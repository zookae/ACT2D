using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SetSprite : MonoBehaviour {

    /// <summary>
    /// Sprite to set object to
    /// </summary>
    public string targetSprite;

    void Update() {
        AssignSprite(targetSprite);
    }

    public void AssignSprite(string spriteName) {
        SpriteRenderer mySprite = gameObject.GetComponent<SpriteRenderer>();
        mySprite.sprite = LoadSprites.Singleton.getSprite(spriteName);
    }
	
}
