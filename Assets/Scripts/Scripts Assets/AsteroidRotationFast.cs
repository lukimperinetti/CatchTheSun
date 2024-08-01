using UnityEngine;

public class AsteroidRotationFast : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private Vector3 randomRotationAxis;

    void Start()
    {
        // Generate a random rotation axis
        randomRotationAxis = Random.insideUnitSphere;
    }

    void Update()
    {
        // Rotate the asteroid around the random axis
        transform.Rotate(randomRotationAxis * rotationSpeed * Time.deltaTime);
    }
}
