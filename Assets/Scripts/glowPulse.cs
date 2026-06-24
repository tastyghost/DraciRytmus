using UnityEngine;

public class PulseGlow : MonoBehaviour
{
    public float speed = 2f;
    public float minScale = 0.9f;
    public float maxScale = 1.1f;

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;

        float scale = Mathf.Lerp(minScale, maxScale, t);

        transform.localScale = new Vector3(scale, scale, 1);
        Debug.Log("PulseGlow Update called. Current scale: " + scale);
    }
}