
using UnityEngine;
using UnityEngine.UI;

public class MainTouchLogic : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float myScore;
    [SerializeField] private float scoreToAdd;

    private bool _began;
    private bool _moved;
    private bool _end;
    private bool _hasMoved;
 
    void Start ()
    {  
        Input.simulateMouseWithTouches = true;
    }
   
    void Update()
    {
        
        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                _began = true;
                _end = false;
                _moved = false;
            }
            if ( touch.phase == TouchPhase.Moved)
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
            _hasMoved = true;
        }
        if (_end)
        {
            Rotate();
        }

        if (_hasMoved && !_moved) // If the move distanced is above 0, that means they have swipped.
        {
            AddScore();
            _hasMoved = false;
        }
    }



    private void Rotate()
    {
        transform.Rotate(Vector3.down, moveSpeed * Time.deltaTime);
    }

    private void AddScore()
    {
        myScore += scoreToAdd;
    }
}
 