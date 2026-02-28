using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 rotationVector;

    void Update()
    {
        float newRotationSpeed = rotationSpeed * 100;
        transform.Rotate(rotationVector * (newRotationSpeed * Time.deltaTime));
    }
}
