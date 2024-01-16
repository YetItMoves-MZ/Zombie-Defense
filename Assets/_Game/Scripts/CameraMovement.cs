using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Speed")]
    public float MoveSpeed;
    public float RotationSpeed;
    public float ZoomSpeed;

    [Header("MinMax")]
    public float MinZoom;
    public float MaxZoom;
    public float MinMovementX;
    public float MaxMovementX;
    public float MinMovementZ;
    public float MaxMovementZ;

    float yPosition;


    void Start()
    {
        yPosition = transform.position.y;
        StartCoroutine(UnscaledUpdate());
    }

    // makeing my own update that is not scaled by unity normal time.
    // private void FixedUpdate()
    // {
    //     Move();
    //     Rotate();
    //     Zoom();
    // }

    IEnumerator UnscaledUpdate()
    {
        while (true)
        {
            Move();
            Rotate();
            Zoom();
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal * Time.unscaledDeltaTime * MoveSpeed, 0f, vertical * Time.unscaledDeltaTime * MoveSpeed);
        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, rotation.z);
        transform.Translate(movement);
        transform.rotation = Quaternion.Euler(rotation);
        if (transform.position.x <= MinMovementX)
            transform.position = new Vector3(MinMovementX, transform.position.y, transform.position.z);
        if (transform.position.x >= MaxMovementX)
            transform.position = new Vector3(MaxMovementX, transform.position.y, transform.position.z);
        if (transform.position.z <= MinMovementZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, MinMovementZ);
        if (transform.position.z >= MaxMovementZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, MaxMovementZ);
    }
    void Rotate()
    {
        float yRotation = Input.GetAxisRaw("Rotate");

        Vector3 rotation = new Vector3(0f, yRotation * Time.unscaledDeltaTime * RotationSpeed, 0f);
        transform.rotation = Quaternion.Euler(rotation + transform.rotation.eulerAngles);
    }
    void Zoom()
    {
        float zoom = Input.GetAxisRaw("Zoom");

        if ((transform.position.y <= MinZoom && zoom > 0) ||
        (transform.position.y >= MaxZoom && zoom < 0))
            zoom = 0;

        transform.Translate(0f, 0f, zoom * Time.unscaledDeltaTime * ZoomSpeed);
    }
}
