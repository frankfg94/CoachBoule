using UnityEngine;

public class DragObject3D : MonoBehaviour
{
    public LayerMask layerMask;  // les couches à raycaster
    public GameObject boule;
    public Camera cam;

    Vector3 startPos;
    Vector3 dist;

    void OnMouseDown()
    {
        startPos = Camera.main.WorldToScreenPoint(transform.position);
        dist = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z));
    }

    void OnMouseDrag()
    {
        Vector3 lastPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(lastPos) + dist;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject == boule)
                {
                    // Faire quelque chose avec l'objet 3D ici
                    Debug.Log("L'objet 3D a été cliqué !");
                    Rigidbody rb = boule.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Renderer renderer = rb.GetComponent<Renderer>();
                        renderer.material.color = Random.ColorHSV();
                    }
                }
            }
        }
    }
}
