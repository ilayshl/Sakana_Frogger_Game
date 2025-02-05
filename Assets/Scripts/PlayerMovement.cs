using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementCollider movementCollider;
    [SerializeField] private Transform movementGrid;
    private bool isMoving = false;
    private bool isJumping = false;

    private Animator animator;

    private const int GRID = 2;
    private const int MINJUMPSPEED = 3; //per GRID

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    private void Start() {
        movementCollider.transform.position = transform.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                InitiateMovement(Input.GetAxisRaw("Horizontal"), 0);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") != 0)
            {
                InitiateMovement(0, Input.GetAxisRaw("Vertical"));
            }
        }
        else if (isMoving)
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                if (!isJumping) { isJumping = true; }
            }
        }
    }

    private void LateUpdate()
    {
        if (isJumping)
        {
            MovePlayer(movementCollider.GetTargetPosition());
        }
    }

    private void InitiateMovement(float x, float y)
    {
        isMoving = true;
        Vector3 offset = new Vector3(GRID * x, GRID * y, 0);
        movementCollider.transform.position =  transform.position + offset;
        movementCollider.SetDirection(x, y);
    }

    private void MovePlayer(Vector3 targetPosition)
    {
        if (Vector2.Distance(transform.position, targetPosition) == 0)
        {
            ResetMovement();
            movementGrid.position = targetPosition;
        }
        else
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            float jumpSpeed = Mathf.Max(MINJUMPSPEED * Time.deltaTime, MINJUMPSPEED * distance * Time.deltaTime);
            transform.position =
            Vector3.MoveTowards(transform.position, targetPosition, jumpSpeed);
        }
    }

    private void ResetMovement()
    {
        isMoving = false;
        isJumping = false;
    }

}
