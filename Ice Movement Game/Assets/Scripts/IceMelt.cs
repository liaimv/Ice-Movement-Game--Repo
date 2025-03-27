using StarterAssets;
using UnityEngine;

public class IceMelt : MonoBehaviour
{
    public float meltAmount = 0f;
    private float meltSpeed;
    public float meltSlowSpeed = 0.1f;
    public float meltFastSpeed = 0.1f;
    private Vector3 originalScale;
    private Vector3 originalPosition;

    private ThirdPersonController controllerScript;
    private Sliding slidingScript;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;

        controllerScript = GetComponentInParent<ThirdPersonController>();
        slidingScript = GetComponentInParent<Sliding>();

        meltSpeed = meltSlowSpeed;
    }

    void Update()
    {
        if (meltAmount < 0.95f)
        {
            meltAmount += Time.deltaTime * meltSpeed;
            MeltIceCube();
        }
        else if (meltAmount > 0.95f)
        {
            controllerScript.MoveSpeed = 0;
            controllerScript.SprintSpeed = 0;
            controllerScript.JumpHeight = 0;
            controllerScript._rotationVelocity = 0;
            slidingScript.moveSpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            meltSpeed = meltFastSpeed; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            meltSpeed = meltSlowSpeed;
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
