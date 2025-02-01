using UnityEngine;

public class MovementCollider : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    private Vector3 startingPosition;
    private Transform targetPosition;
    Vector3 dir;

    private const int GRID = 2;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(Vector3.Distance(startingPosition, transform.position) <= GRID){
        transform.position+=dir*movementSpeed*Time.deltaTime;
        }
        else{
            Debug.Log("Can't move anymore");
        }
    }

    public void SetDirection(float x, float y){
        dir = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        targetPosition = other.transform;
        Debug.Log("New collision at " + other.transform);
    }

    public Vector3 GetTargetPosition(){
        return targetPosition.position;
    }
}
