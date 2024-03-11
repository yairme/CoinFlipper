using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] private GameObject coinObject; // Define the coin object.
    [SerializeField] private float rotationSpeed; // Define the rotation speed.
    [SerializeField] private Vector3 rotationVelocity;  // Define the rotation velocity.
    [SerializeField] float reductionFactor; // Adjust this value to control the speed of reduction.
    [SerializeField] float returnSpeed; // Define a speed for the rotation to return to zero.
    [SerializeField] private int score; // Initialize score to 0.
    [SerializeField] private int pointsPerSwipe; // Define how many points to add per swipe.
    [SerializeField] private float rotationVelocityDecreaseTreshold; // Define the magnitude of the rotation velocity.

    private bool _isRotating; // Define a boolean to check if the coin is rotating.
    private Vector3 _previousMousePosition; // Define a vector to store the previous mouse position.

    void Update()
    {   
        // Check if the user has clicked on the coin.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == coinObject)
            {
                _isRotating = true;
                _previousMousePosition = Input.mousePosition;
            }
        }
        // Check if the user has released the mouse.
        else if (Input.GetMouseButtonUp(0))
        {
            Touch touch = Input.GetTouch(0);
            _isRotating = false;
            if (touch.phase == TouchPhase.Moved)
            {
                rotationVelocity = new Vector3(-Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0f) * rotationSpeed; 
            }

            if (rotationVelocity.magnitude < 1f) 
            {
                score += pointsPerSwipe;
            }   
        }

      

        // If the user is rotating the coin, rotate it based on the mouse movement.
        if (_isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - _previousMousePosition;

            // Calculate the rotation axis.
            Vector3 rotationAxis = Vector3.Cross(mouseDelta, Camera.main.transform.forward);

            // Rotate the coin around the calculated axis.
            transform.Rotate(rotationAxis, mouseDelta.magnitude, Space.World);

            _previousMousePosition = currentMousePosition;
        }
        // If the user is not rotating the coin, rotate it based on the rotation velocity.
        else
        {
            transform.Rotate(rotationVelocity * Time.deltaTime, Space.World);
        }

        // Decrease rotationVelocity only when there is velocity
        if (rotationVelocity.magnitude > rotationVelocityDecreaseTreshold) // Use a small threshold instead of checking for exact zero.
        {
            rotationVelocity = Vector3.Lerp(rotationVelocity, Vector3.zero, reductionFactor * Time.deltaTime);
        }

        // If the rotation velocity is very small, set it to zero and interpolate the rotation towards the identity rotation.
        if (rotationVelocity.magnitude < rotationVelocityDecreaseTreshold)
        {
            rotationVelocity = Vector3.zero;

            // Interpolate from the current rotation towards the identity rotation.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime);
        }
    }

}
