using UnityEngine;

public class IceMelt : MonoBehaviour
{
    private float meltAmount = 0f;
    public float meltSpeed = 0.2f;
    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (meltAmount < 0.95f)
        {
            meltAmount += Time.deltaTime * meltSpeed;
            MeltIceCube();
        }
    }

    void MeltIceCube()
    {
        float newYScale = Mathf.Lerp(originalScale.y, 0f, meltAmount);

        float heightDifference = originalScale.y - newYScale;
        float newYPosition = originalPosition.y - heightDifference;

        transform.localScale = new Vector3(originalScale.x, newYScale, originalScale.z);
        transform.localPosition = new Vector3(originalPosition.x, newYPosition, originalPosition.z);
    }
}
