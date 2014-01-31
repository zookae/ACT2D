using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(Collider2D))]
public class ClickDragMouseReturnSnap : MonoBehaviour 
{
 
private Vector3 screenPoint;
private Vector3 offset;
private Vector2 origin;


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
    transform.position = origin;
}
 
}