using UnityEngine;
using System.Collections;
using System;

public class LoadSprites : MonoBehaviour {

#region singleton
    // cf: http://clearcutgames.net/home/?p=437
    // (v1) Allow manipulation in editor and prevent duplicates
    // static singleton property
    public static LoadSprites Singleton { get; private set; }

    // instantiate on game start
    void Awake() {

        // check for conflicting instances
        if (Singleton != null && Singleton != this) {
            Destroy(gameObject); // destroy others that conflict
        }

        Singleton = this; // save singleton instance

        DontDestroyOnLoad(gameObject); // ensure not destroyed b/t scenes

        // cf: http://answers.unity3d.com/questions/576153/loading-a-sprite-unity-43-in-resource-folder-and-s.html
        // load sprites up
        Singleton.spriteDB = Resources.LoadAll<Sprite>(spritePath);
        Singleton.spriteNames = new string[Singleton.spriteDB.Length];

        for (int ii = 0; ii < Singleton.spriteNames.Length; ii++) {
            Singleton.spriteNames[ii] = Singleton.spriteDB[ii].name;
            //Debug.Log("[LoadSprite]: loading " + Singleton.spriteNames[ii]);
        }
    }
#endregion

    /// <summary>
    /// Store all sprites loaded from path
    /// </summary>
    public Sprite[] spriteDB;

    /// <summary>
    /// Names of sprites in the DB
    /// </summary>
    private string[] spriteNames;


    /// <summary>
    /// Path to folder containing sprites under "Resources" folder
    /// </summary>
    public string spritePath = "textures";

    
    /// <summary>
    /// Returns the sprite with the given name
    /// </summary>
    /// <param name="spriteName"></param>
    /// <returns></returns>
    public Sprite getSprite(string spriteName) {
        Sprite sprite = Singleton.spriteDB[Array.IndexOf(Singleton.spriteNames, spriteName)];
        return sprite;
    }

    /// <summary>
    /// Returns the sprite at the given index in the database
    /// </summary>
    /// <param name="spriteIdx"></param>
    /// <returns></returns>
    public Sprite getSprite(int spriteIdx) {
        Sprite sprite = Singleton.spriteDB[spriteIdx];
        return sprite;
    }

    /// <summary>
    /// Returns a randomly selected sprite from the database
    /// </summary>
    /// <returns></returns>
    public Sprite getSpriteRandom() {
        Sprite sprite = Singleton.spriteDB[UnityEngine.Random.Range(0, spriteDB.Length - 1)];
        return sprite;
    }
    
}
