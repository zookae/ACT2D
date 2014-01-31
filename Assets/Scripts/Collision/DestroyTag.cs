using UnityEngine;
using System.Collections;

public class DestroyTag : MonoBehaviour {

    public string destroyTag;

    /// <summary>
    /// Destroy objects with a given tag
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("destroy : entered trigger");
        if (col.CompareTag(destroyTag)) {
            Destroy(col.gameObject);
        }
        
    }
}
