using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(Collider2D))]
public class ClickDragMouseReturnSmooth : MonoBehaviour 
{
 
private Vector3 screenPoint;
private Vector3 offset;
private Vector2 origin;

private bool isReturning = false;
public float returnTime = 0.03f;

void Start() {
    origin = transform.position;
}
void OnMouseDown()
{
    screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
 
}
 
void OnMouseDrag()
{
    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
	Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
	transform.position = curPosition;
 
}

void OnMouseUp() {
    isReturning = true;
}

void Update() {
    if (isReturning) {
        transform.position = Vector3.Lerp(transform.position, origin, Time.deltaTime/returnTime);
    }
    if (Mathf.Abs(Vector3.Distance(transform.position, origin)) < 0.1) {
        isReturning = false;
    }
}
 
}