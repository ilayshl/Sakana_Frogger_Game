using UnityEngine;

public class MovementCollider : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    private Transform targetPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        targetPosition = other.transform;
    }
}
