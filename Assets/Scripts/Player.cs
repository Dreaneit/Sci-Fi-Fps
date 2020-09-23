using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    private Vector3 playerVelocity;
    private float yVelocity;
    private bool canDoubleJump;
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = 9.81f;
    private GameObject muzzleFlash;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        muzzleFlash = GameObject.Find("LookY/Main Camera/Weapon/Muzzle_Flash").gameObject;

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }

        if (Input.GetMouseButton((int)MouseButton.LeftMouse))
        {
            muzzleFlash.SetActive(true);
        }
        else
        {
            muzzleFlash.SetActive(false);
        }

        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(screenCenter);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("hit: " +hitInfo.transform.name);
            }
        }

        HandleMovement();
    }
    private void HandleMovement()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalAxis, 0, verticalAxis);
        playerVelocity = direction * playerSpeed;

        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpHeight;
            }
        }
        else
        {
            yVelocity -= gravityValue;
        }

        playerVelocity.y = yVelocity;

        playerVelocity = transform.transform.TransformDirection(playerVelocity);

        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
