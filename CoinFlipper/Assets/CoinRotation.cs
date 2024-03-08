using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] private GameObject coinObject;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Vector3 rotationVelocity;
    [SerializeField] float reductionFactor = 1f; // Adjust this value to control the speed of reduction
    [SerializeField] float returnSpeed = 1f; // Define a speed for the rotation to return to zero
    [SerializeField] private int score = 0; // Initialize score to 0
    [SerializeField] private int pointsPerSwipe = 1; // Define how many points to add per swipe

    private bool isRotating;
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
            rotationVelocity = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * rotationSpeed;

            score += pointsPerSwipe;
        }

        if (isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            // Calculate the rotation axis
            Vector3 rotationAxis = Vector3.Cross(mouseDelta, Camera.main.transform.forward);

            // Rotate the coin around the calculated axis
            transform.Rotate(rotationAxis, mouseDelta.magnitude, Space.World);

            previousMousePosition = currentMousePosition;
        }
        else
        {
            transform.Rotate(rotationVelocity * Time.deltaTime, Space.World);
        }

        // Decrease rotationVelocity only when there is velocity
        if (rotationVelocity.magnitude > 0.1f) // Use a small threshold instead of checking for exact zero
        {
            rotationVelocity = Vector3.Lerp(rotationVelocity, Vector3.zero, reductionFactor * Time.deltaTime);
        }

        if (rotationVelocity.magnitude < 0.1f)
        {
            rotationVelocity = Vector3.zero;

            // Interpolate from the current rotation towards the identity rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime);
        }
    }

}
