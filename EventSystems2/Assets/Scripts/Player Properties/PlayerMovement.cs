using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    //
    public float moveSpeed = 0.05f;
    public float rotationOffset;
    //
    private CharacterController characterController;
    private void Awake()
    {
       characterController = GetComponent<CharacterController>(); 
    }

    private void Update()
    {
        //move
        Vector3 movementInput = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        characterController.Move(movementInput * moveSpeed);

        RotateCharacter();

    }

    private void RotateCharacter()
    {
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = -10f; //The distance between the camera and object
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = (Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg) + rotationOffset;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
