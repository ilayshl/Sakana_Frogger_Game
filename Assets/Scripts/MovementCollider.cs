using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovementCollider : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool canMove = false;
    Vector3 dir;

    private const int GRID = 2;

    void Update()
    {
        if (canMove)
        {
            if (Vector3.Distance(startingPosition, transform.position) <= GRID)
            {
                transform.position += dir * movementSpeed * Time.deltaTime;
            }
        }
    }

    public void SetDirection(float x, float y)
    {
        dir = new Vector3(x, y, 0);
        canMove = true;
        startingPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Grid") && canMove == true)
        {
            targetPosition = other.transform.position;
        }
    }

    public Vector3 GetTargetPosition()
    {
        canMove = false;
        transform.position = targetPosition;
        return targetPosition;
    }

}
