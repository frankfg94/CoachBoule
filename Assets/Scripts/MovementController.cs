

using UnityEngine;


public class MovementController : MonoBehaviour
{
    Vector3 startPos;
    Vector3 dist;

    void OnMouseDown()
    {
        startPos = Camera.main.WorldToScreenPoint(transform.position);
        dist = transform.position - TransparentWindow.Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z));
    }

    void OnMouseDrag()
    {
        Vector3 lastPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z);
        transform.position = TransparentWindow.Camera.ScreenToWorldPoint(lastPos) + dist;
    }
}