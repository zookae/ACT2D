using UnityEngine;
using System.Collections;
using System;

public class LoadText : MonoBehaviour {

#region singleton
    // cf: http://clearcutgames.net/home/?p=437
    // (v1) Allow manipulation in editor and prevent duplicates
    // static singleton property
    public static LoadText Singleton { get; private set; }

    // instantiate on game start
    void Awake() {

        // check for conflicting instances
        if (Singleton != null && Singleton != this) {
            Destroy(gameObject); // destroy others that conflict
        }

        Singleton = this; // save singleton instance

        DontDestroyOnLoad(gameObject); // ensure not destroyed b/t scenes

        // load strings up; split on newlines
        TextAsset fileContent = Resources.Load<TextAsset>(textFile);
        Singleton.textDB = fileContent.text.Split('\n');
    }
#endregion

    /// <summary>
    /// Store all strings loaded from path
    /// </summary>
    public string[] textDB;

    /// <summary>
    /// Path to file containing new-line delimited strings.
    /// </summary>
    public string textFile = "BFI_text_simple.txt";

    

    /// <summary>
    /// Returns the string at the index in the database.
    /// </summary>
    /// <param name="strIdx"></param>
    /// <returns></returns>
    public string getText(int strIdx) {
        return (textDB[strIdx]);
    }

    /// <summary>
    /// Retruns a random string from the database.
    /// </summary>
    /// <returns></returns>
    public string getRandomText() {
        return (textDB[UnityEngine.Random.Range(0, textDB.Length - 1)]);
    }
    
}
