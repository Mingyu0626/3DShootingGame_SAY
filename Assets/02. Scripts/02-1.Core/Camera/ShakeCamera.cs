using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private Vector3 _originalPosition;
    private void Awake()
    {
        _originalPosition = transform.localPosition;
    }
    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = _originalPosition + new Vector3(x, y, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = _originalPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        Debug.Log("ShakeCoroutine");
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }
}