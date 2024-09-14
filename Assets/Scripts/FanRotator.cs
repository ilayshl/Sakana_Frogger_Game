
using UnityEngine;

public class FanRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 800f;
    
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed*Time.deltaTime);
    }
}
