using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerController : NetworkBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float groundedGravity = -2f;
    [SerializeField] float jumpHeight = 5f;

    private CharacterController characterController;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);

        Vector2 inputDirection = new Vector2(horizontalInput, verticalInput);

        MovePlayerRpc(inputDirection, jumpPressed);
    }
    [Rpc(SendTo.Server)]
    private void MovePlayerRpc(Vector2 movementInput, bool jumpPressed)
    {
        MovePlayer(movementInput, jumpPressed);
    }

    private void MovePlayer(Vector2 movementInput, bool jumpPressed)
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = groundedGravity;
            }

            if (jumpPressed)
            {
                verticalVelocity = jumpHeight;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        Vector3 horizontalMovement = moveDirection * moveSpeed;
        Vector3 verticalMovement = Vector3.up * verticalVelocity;
        Vector3 finalMovement = horizontalMovement + verticalMovement;
        characterController.Move(finalMovement * Time.deltaTime);

    }
    
}
