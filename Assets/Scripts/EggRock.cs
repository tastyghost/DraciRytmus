using UnityEngine;

public class EggRock : MonoBehaviour
{
    [SerializeField] private float angle = 4f;
    [SerializeField] private float speed = 2f;

    private Quaternion initialLocalRotation;

    private void Awake()
    {
        initialLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        float z = Mathf.Sin(Time.time * speed) * angle;
        transform.localRotation = initialLocalRotation * Quaternion.Euler(0f, 0f, z);
    }

    private void OnDisable()
    {
        transform.localRotation = initialLocalRotation;
    }
}
