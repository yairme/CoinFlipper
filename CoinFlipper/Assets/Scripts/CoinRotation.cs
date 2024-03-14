using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] private GameObject coinObject; // Define the coin object.
    [SerializeField] private float rotationSpeed; // Define the rotation speed.
    [SerializeField] private float rotationSpeedReset;
    [SerializeField] private float rotationReset;
    [SerializeField] private float returnSpeed; // Define a speed for the rotation to return to zero.
    [SerializeField] private float speedCap;
    [SerializeField] private int score; // Initialize score to 0.
    [SerializeField] private int pointsPerSwipe; // Define how many points to add per swipe.
    [SerializeField] private float rotationVelocityDecreaseTreshold; // Define the magnitude of the rotation velocity.
    [SerializeField] private float rotationDecay; // You can adjust this value as needed.

    private bool _isRotated; // Define a boolean to check if the coin is rotated.
    private bool _isRotating; 
    private Vector3 _previousMousePosition; // Define a vector to store the previous mouse position.
    private Vector3 lastUsedRotationAxis;
    private float lastUsedDelta;

    void Update()
    {   
        // Check if the user has clicked on the coin.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == coinObject)
            {
                _isRotated = true;
                _previousMousePosition = Input.mousePosition;

                if (speedCap <= rotationSpeed) rotationSpeed = speedCap; 

                rotationSpeed += rotationSpeedReset;
            }
        }
        // Check if the user has released the mouse.
        else if (Input.GetMouseButtonUp(0))
        {
            _isRotated = false;


            if (lastUsedDelta > 0.5f && lastUsedRotationAxis.magnitude > 0.5f)
            {
                score += pointsPerSwipe;
            }
        }

        // If the user is rotating the coin, rotate it based on the mouse movement.
        if (_isRotated)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - _previousMousePosition;

            // Calculate the rotation axis.
            Vector3 rotationAxis = Vector3.Cross(mouseDelta, Camera.main.transform.forward);

            // Rotate the coin around the calculated axis.
            transform.Rotate(rotationAxis, mouseDelta.magnitude, Space.World);
            lastUsedRotationAxis = rotationAxis;
            lastUsedDelta = mouseDelta.magnitude;

            _previousMousePosition = currentMousePosition;

        }
        // If the user is not rotating the coin, rotate it based on the rotation velocity.
        else
        {
            _isRotating = true;
            transform.Rotate(lastUsedRotationAxis, rotationSpeed * Time.deltaTime, Space.World);
        }

        // Decrease rotationVelocity only when there is velocity
        if (rotationSpeed > rotationReset && _isRotating) // Use a small threshold instead of checking for exact zero.
        {
        
            // Decrease the rotation speed.
            rotationSpeed -= rotationDecay * Time.deltaTime;

        }

    }
    

}
