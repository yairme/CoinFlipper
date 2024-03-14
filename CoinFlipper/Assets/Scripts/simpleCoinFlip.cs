
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the main logic for handling touch input and controlling the rotation and scoring of a game object.
/// </summary>
public class MainTouchLogic : MonoBehaviour
{
    // Score Settings
    [Header("Score Settings")]
    /// <summary>
    /// The current score of the game object
    /// </summary>
    [SerializeField] private float myScore; 
    /// <summary>
    /// The score to add to the current score
    /// </summary>
    [SerializeField] private float scoreToAdd; 

    // Rotated Amount Settings
    [Header("Rotated Amount Settings")]
    /// <summary>
    /// A counter for the rotated amount of the game object so it can reset
    /// </summary>
    [SerializeField] private float rotatedAmount; 
    /// <summary>
    /// The rotated amount to reset the game object position
    /// </summary>
    [SerializeField] private float rotatedAmountToReset; 
    /// <summary>
    /// The counter to reset the game object position
    /// </summary>
    [SerializeField] private float resetRotatedAmount; 

    // Rotation Speed Settings
    [Header("Rotation Speed Settings")]
    /// <summary>
    /// The current move speed of the game object
    /// </summary>
    [SerializeField] private float moveSpeed; 
    /// <summary>
    /// The return rotation speed of the game object so it can reset position
    /// </summary>
    [SerializeField] private float returnRotationSpeed; 
    /// <summary>
    /// The max rotation speed of the game object so it doesn't rotate too fast
    /// </summary>
    [SerializeField] private float maxRotationSpeed; 
    /// <summary>
    /// The add rotation speed of the game object so it moves faster
    /// </summary>
    [SerializeField] private float addRotationSpeed; 

    // Max Rotation Settings
    [Header("Max Rotation Settings")]
    /// <summary>
    /// The rotated max check of the game object so it can reset
    /// </summary>
    [SerializeField] private float rotatedMaxCheck; 
    /// <summary>
    /// The add max rotation of the game object so it can keep rotating
    /// </summary>
    [SerializeField] private float addMaxRotation;

    // Score Properties with getters and setters
    /// <summary>
    /// The current score of the game object.
    /// </summary>
    public float MyScore { get => myScore; set => myScore = value; } 

    /// <summary>
    /// The score to add to the current score.
    /// </summary>
    public float ScoreToAdd { get => scoreToAdd; set => scoreToAdd = value; }

    // Rotated Amount Properties with getters and setters
    /// <summary>
    /// A counter for the rotated amount of the game object so it can reset.
    /// </summary>
    public float RotatedAmount { get => rotatedAmount; set => rotatedAmount = value; }

    /// <summary>
    /// The rotated amount to reset the game object position.
    /// </summary>
    public float RotatedAmountToReset { get => rotatedAmountToReset; set => rotatedAmountToReset = value; }

    /// <summary>
    /// The counter to reset the game object position.
    /// </summary>
    public float ResetRotatedAmount { get => resetRotatedAmount; set => resetRotatedAmount = value; }

    // Rotation Speed Properties with getters and setters
    /// <summary>
    /// The current move speed of the game object.
    /// </summary>
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    /// <summary>
    /// The return rotation speed of the game object so it can reset position.
    /// </summary>
    public float ReturnRotationSpeed { get => returnRotationSpeed; set => returnRotationSpeed = value; }

    /// <summary>
    /// The max rotation speed of the game object so it doesn't rotate too fast.
    /// </summary>
    public float MaxRotationSpeed { get => maxRotationSpeed; set => maxRotationSpeed = value; }

    /// <summary>
    /// The add rotation speed of the game object so it moves faster.
    /// </summary>
    public float AddRotationSpeed { get => addRotationSpeed; set => addRotationSpeed = value; }

    // Max Rotation Properties with getters and setters
    /// <summary>
    /// The rotated max check of the game object so it can reset.
    /// </summary>
    public float RotatedMaxCheck { get => rotatedMaxCheck; set => rotatedMaxCheck = value; }

    /// <summary>
    /// The add max rotation of the game object so it can keep rotating.
    /// </summary>
    public float AddMaxRotation { get => addMaxRotation; set => addMaxRotation = value; }

    // Private fields
    private bool _began;
    private bool _moved;
    private bool _end;
    private bool _hasRotated;

    /// <summary>
    /// Initializes the touch input simulation.
    /// </summary>
    void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    /// <summary>
    /// Handles touch input and updates the rotation and scoring of the game object.
    /// </summary>
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _began = true;
                _moved = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                _began = false;
                _moved = true;
                _end = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _began = false;
                _moved = false;
                _end = true;
            }
        }

        if (_began)
        {
            
        }
        if (_moved)
        {
            if (moveSpeed <= maxRotationSpeed)
            {
                moveSpeed += addRotationSpeed;
            }

            if (rotatedAmount >= -rotatedMaxCheck)
            {
                rotatedAmount -= addMaxRotation;
            }
        }
        if (_end)
        {
            _hasRotated = false;
            rotatedAmount++;
            resetRotatedAmount = 0;
        }

        if (_end && !_hasRotated)
        {
            Rotate();
            AddScore();

            if (rotatedAmount >= rotatedMaxCheck)
            {
                rotatedAmount = 0;
                _hasRotated = true;
                _end = false;
            }
        }

        if (_hasRotated)
        {
            resetRotatedAmount++;
        }

        if (resetRotatedAmount >= rotatedAmountToReset)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * returnRotationSpeed);
            if (transform.rotation.eulerAngles.y == 0)
            {
                resetRotatedAmount = 0;
            }
        }
    }

    /// <summary>
    /// Rotates the game object based on the current move speed.
    /// </summary>
    private void Rotate()
    {
        transform.Rotate(Vector3.down, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Adds the score to the current score.
    /// </summary>
    private void AddScore()
    {
        myScore += scoreToAdd;
    }
}
 