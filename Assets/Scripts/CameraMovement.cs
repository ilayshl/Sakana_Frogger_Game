using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float movementSpeed;

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10) + offset;
        float distance = Vector3.Distance(currentPosition, targetPosition);
        transform.position = 
        Vector3.MoveTowards(currentPosition, targetPosition, movementSpeed * Time.deltaTime * distance);
    }

    public void ChangeOffset(Vector3 change){
        offset+=change;
    }
}
