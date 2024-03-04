using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject coinObject;

    private bool isRotating = false;
    private Vector3 previousMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == coinObject)
            {
                isRotating = true;
                previousMousePosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            transform.Rotate(Vector3.up, mouseDelta.x, Space.World);
            transform.Rotate(Vector3.right, -mouseDelta.y, Space.World);

            previousMousePosition = currentMousePosition;
        }
    }
}
