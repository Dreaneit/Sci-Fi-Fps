﻿using System.Collections;
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

    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private GameObject hitMarker;
    [SerializeField]
    private AudioSource weaponAudio;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        currentAmmo = maxAmmo;

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

        if (currentAmmo > 0 && Input.GetMouseButton((int)MouseButton.LeftMouse))
        {
            Shoot();
        }
        else
        {
            muzzleFlash.SetActive(false);
            weaponAudio.Stop();
        }

        if (Input.GetKeyDown(KeyCode.R) && !Input.GetMouseButton((int)MouseButton.LeftMouse))
        {
            StartCoroutine(Reloading());
        }

        HandleMovement();
    }

    IEnumerator Reloading()
    {
        Debug.Log("enumerator");
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
    }

    private void Shoot()
    {
        muzzleFlash.SetActive(true);
        currentAmmo--;

        if (!weaponAudio.isPlaying)
        {
            weaponAudio.Play();
        }

        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);
        Ray rayOrigin = Camera.main.ViewportPointToRay(screenCenter);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            var hitMarkerObject = Instantiate(hitMarker, hitInfo.point, Quaternion.identity);
            Destroy(hitMarkerObject, 1f);
        }
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
