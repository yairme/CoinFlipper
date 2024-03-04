using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Called every frame to check for user input and rotate the coin accordingly.
/// </summary>
public class CoinRotation : MonoBehaviour
{
    [SerializeField] private GameObject coinObject;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Vector3 rotationVelocity;

    private bool isRotating;
    private Vector3 previousMousePosition;

    /// <summary>
    /// Checks for user input and updates the rotation of the coin.
    /// </summary>
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
            rotationVelocity = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * rotationSpeed;
        }

        if (isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            transform.Rotate(Vector3.up, mouseDelta.x, Space.World);
            transform.Rotate(Vector3.right, -mouseDelta.y, Space.World);

            previousMousePosition = currentMousePosition;

            // Decrease rotationVelocity until it reaches 0
            rotationVelocity = Vector3.Lerp(rotationVelocity, Vector3.zero, Time.deltaTime);
        }
        else
        {
            transform.Rotate(rotationVelocity * Time.deltaTime, Space.World);
        }
    }
}
