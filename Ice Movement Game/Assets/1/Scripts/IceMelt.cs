using UnityEditor.PackageManager.Requests;
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

    public Canvas gameOverCanvas;
    public Canvas gameWinCanvas;

    private float originalPlayerSpeed;
    private float originalJumpForce;
    private float originalRotationSpeed;

    private Vector3 originalPosition;
    private Vector3 checkpointPosition;

    void Start()
    {
        controllerScript = GetComponentInParent<PlayerController>();
        originalScale = transform.localScale.x;
        meltSpeed = OGmeltSpeed;

        originalPlayerSpeed = controllerScript.playerSpeed;
        originalJumpForce = controllerScript.jumpForce;
        originalRotationSpeed = controllerScript.rotationSpeed;

        originalPosition = transform.position;
        checkpointPosition = originalPosition;

        gameOverCanvas.gameObject.SetActive(false);
        gameWinCanvas.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (transform.localScale.x > 0.05f)
        {
            meltTimer += Time.deltaTime * meltSpeed * meltTimerMultiplier; //Unity doesn't like small numbers, so I multiply the meltSpeed with the meltTimerMultiplier
            meltAmount = meltCurve.Evaluate(meltTimer);
            MeltIceCube();

            gameOverCanvas.gameObject.SetActive(false);
        }
        else
        {
            controllerScript.playerSpeed = 0;
            controllerScript.jumpForce = 0;
            controllerScript.rotationSpeed = 0;

            gameOverCanvas.gameObject.SetActive(true);

            transform.position = checkpointPosition;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stage2"))
        {
            checkpointPosition = transform.position;
            ResetSize();
        }
        if (other.gameObject.CompareTag("Stage3"))
        {
            checkpointPosition = transform.position;
            ResetSize();
        }
        if (other.gameObject.CompareTag("Stage4"))
        {
            checkpointPosition = transform.position;
            ResetSize();
        }

        if (other.gameObject.CompareTag("Win"))
        {
            gameWinCanvas.gameObject.SetActive(true);
            ResetSize();
        }
    }

    public void ResetGame()
    {

        ResetSize();
        controllerScript.playerSpeed = originalPlayerSpeed;
        controllerScript.jumpForce = originalJumpForce;
        controllerScript.rotationSpeed = originalRotationSpeed;
    }

    private void ResetSize()
    {
        meltAmount = 0f;
        meltTimer = 0f;
        transform.localScale = new Vector3(originalScale, originalScale, originalScale);
    }

    void MeltIceCube()
    {
        float newScale = Mathf.Lerp(originalScale, 0f, meltAmount);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
