
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{

    Collision objCollision;
    [SerializeField] float dragSpeed = 1.5f;
    [SerializeField] bool moveRight = true;

    private void Start() {
    }

    void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Plate") ||collision.gameObject.CompareTag("Player")){
        Vector3 drag = (moveRight ? Vector3.right : Vector3.left)*Time.deltaTime*dragSpeed;
        collision.transform.position += drag;
    }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Plate")) {
            Vector3 drag = (moveRight ? Vector3.right : Vector3.left)*-22;
            collision.transform.position += drag;
        }
    }
}
