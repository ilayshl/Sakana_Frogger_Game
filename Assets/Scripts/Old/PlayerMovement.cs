using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] MovementCollider movementCollider;
    private MovementCollider activeMovementCollider = null;

    private const int GRID = 2;

    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal") + ", " + Input.GetAxisRaw("Vertical"));
        if (activeMovementCollider == null)
        {
            if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 0)
            {
                InitiateMovement(Input.GetAxisRaw("Horizontal"), 0);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1)
            {
                InitiateMovement(0, Input.GetAxisRaw("Vertical"));
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 1 && Input.GetAxisRaw("Vertical") < 1)
        {
            MovePlayer(activeMovementCollider.GetTargetPosition());
            Destroy(activeMovementCollider.gameObject);
            activeMovementCollider = null;
        }

        //When pressing WASD, a movementCollider instantiates in the direction chosen.
        //When key is held, the movementCollider keeps going in the same direction.
        //When released, the movementCollider locks in the nearest Position Checker and the fish jumps to it.
        //When fish arrives, destroy MovementCollider.
    }

    private void InitiateMovement(float x, float y)
    {
        Vector3 offset = new Vector3(GRID * x, GRID * y, 0);
        activeMovementCollider = Instantiate(movementCollider, transform.position + offset, Quaternion.identity);
        activeMovementCollider.SetDirection(x, y);
    }

    private void MovePlayer(Vector3 position)
    {
        transform.position = position;
    }

}
