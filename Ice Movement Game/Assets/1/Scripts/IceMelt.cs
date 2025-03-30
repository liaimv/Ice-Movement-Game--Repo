using UnityEngine;

public class IceMelt : MonoBehaviour
{
    private float meltAmount = 0f;
    private float meltTimer = 0f;
    public AnimationCurve meltCurve;

    public float OGmeltSpeed = 0.1f;
    private float meltSpeed;
    public float meltSlowSpeed = 0.01f;
    public float meltFastSpeed = 0.5f;
    private float meltTimerMultiplier = 0.01f;

    private float originalScale;

    private PlayerController controllerScript;

    void Start()
    {
        controllerScript = GetComponentInParent<PlayerController>();
        originalScale = transform.localScale.x;
        meltSpeed = OGmeltSpeed;
    }

    void FixedUpdate()
    {
        if (transform.localScale.x > 0.05f)
        {
            meltTimer += Time.deltaTime * meltSpeed * meltTimerMultiplier;
            meltAmount = meltCurve.Evaluate(meltTimer);
            MeltIceCube();
        }
        else
        {
            controllerScript.playerSpeed = 0;
            controllerScript.jumpForce = 0;
            controllerScript.rotationSpeed = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cold"))
        {
            meltSpeed = meltSlowSpeed; 
        }
        
        if (collision.gameObject.CompareTag("Hot"))
        {
            meltSpeed = meltFastSpeed;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cold"))
        {
            meltSpeed = OGmeltSpeed;
        }
        
        if (collision.gameObject.CompareTag("Hot"))
        {
            meltSpeed = OGmeltSpeed;
        }
    }

    void MeltIceCube()
    {
        float newScale = Mathf.Lerp(originalScale, 0f, meltAmount);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
