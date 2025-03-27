using UnityEngine;

public class Sliding : MonoBehaviour
{
    public Camera mainCamera;
    public float moveSpeed = 2f;         
    public float slideFriction = 1f;     
    public float inputThreshold = 0.225f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lastMoveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Get camera-relative movement direction (ignore vertical movement)
        Vector3 inputDir = v * mainCamera.transform.forward + h * mainCamera.transform.right;
        inputDir.y = 0;
        inputDir.Normalize();

        float inputMagnitude = Mathf.Min(new Vector3(h, 0, v).sqrMagnitude, 1f);

        if (inputMagnitude > inputThreshold)
        {
            // Active input — update last direction and move at full speed
            lastMoveDirection = inputDir * moveSpeed;
        }
        else
        {
            // No input — apply sliding friction to gradually slow down
            lastMoveDirection = Vector3.Lerp(lastMoveDirection, Vector3.zero, slideFriction * Time.deltaTime);
        }

        // Apply movement
        controller.Move(lastMoveDirection * Time.deltaTime);
    }
}
