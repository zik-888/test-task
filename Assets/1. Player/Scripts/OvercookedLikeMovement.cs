using UnityEngine;

public class OvercookedLikeMovement : MonoBehaviour {

    public float speed = 2f;

    private Vector3 _moveVector;

    void Update () {
        HandleInput();

        transform.Rotate(Vector3.up, Angle360(transform.forward, _moveVector, transform.right));

        transform.Translate(
            Moves() * transform.forward * speed * Time.deltaTime,
            Space.World
        );
    }

    void HandleInput() {
        _moveVector.x = Input.GetAxis("Horizontal");
        _moveVector.z = Input.GetAxis("Vertical");
    }

    float Moves() {
        if(_moveVector.x != 0 || _moveVector.z != 0)
            return 1f;
        else
            return 0f;
    }

    float Angle360(Vector3 from, Vector3 to, Vector3 right) {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) > 90f) ? 360f - angle : angle;
    }
}