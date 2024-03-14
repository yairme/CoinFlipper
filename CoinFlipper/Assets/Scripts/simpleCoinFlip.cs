
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the main logic for handling touch input and controlling the rotation and scoring of a game object.
/// </summary>
public class MainTouchLogic : MonoBehaviour
{
    // Score Settings
    [Header("Score Settings")]
    [SerializeField] private float myScore;
    [SerializeField] private float scoreToAdd;

    // Rotated Amount Settings
    [Header("Rotated Amount Settings")]
    [SerializeField] private float rotatedAmount;
    [SerializeField] private float rotatedAmountToReset;
    [SerializeField] private float resetRotatedAmount;

    // Rotation Speed Settings
    [Header("Rotation Speed Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float returnRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float addRotationSpeed;

    // Max Rotation Settings
    [Header("Max Rotation Settings")]
    [SerializeField] private float rotatedMaxCheck;
    [SerializeField] private float addMaxRotation;

    // Properties
    public float MyScore { get => myScore; set => myScore = value; }
    public float ScoreToAdd { get => scoreToAdd; set => scoreToAdd = value; }
    public float RotatedAmount { get => rotatedAmount; set => rotatedAmount = value; }
    public float RotatedAmountToReset { get => rotatedAmountToReset; set => rotatedAmountToReset = value; }
    public float ResetRotatedAmount { get => resetRotatedAmount; set => resetRotatedAmount = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float ReturnRotationSpeed { get => returnRotationSpeed; set => returnRotationSpeed = value; }
    public float MaxRotationSpeed { get => maxRotationSpeed; set => maxRotationSpeed = value; }
    public float AddRotationSpeed { get => addRotationSpeed; set => addRotationSpeed = value; }
    public float RotatedMaxCheck { get => rotatedMaxCheck; set => rotatedMaxCheck = value; }
    public float AddMaxRotation { get => addMaxRotation; set => addMaxRotation = value; }
    public bool Began { get => _began; set => _began = value; }
    public bool Moved { get => _moved; set => _moved = value; }
    public bool End { get => _end; set => _end = value; }
    public bool HasRotated { get => _hasRotated; set => _hasRotated = value; }

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
 