using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 15f;

    private Vector2 _startRotation = new Vector2();

    private bool _isGrounded;
    private Rigidbody _rb;
    private Transform _cameraTransform;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;

        _startRotation.x = transform.rotation.y;
        _startRotation.y = _cameraTransform.rotation.x;
    }


    private void FixedUpdate()
    {
        MovementLogic();
    }
    
    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * (speed * Time.fixedDeltaTime));
        
        var rotateX = Vector3.up * (_startRotation.x + Input.GetAxis("Mouse X") * rotateSpeed * Time.fixedDeltaTime);
        transform.Rotate(rotateX);
        
        var rotateY = Vector3.left * (_startRotation.y + Input.GetAxis("Mouse Y") * rotateSpeed * Time.fixedDeltaTime);
        _cameraTransform.Rotate(rotateY);
    }

    public void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpdate(collision, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGroundedUpdate(collision, false);
    }

    private void IsGroundedUpdate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}