using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Zoom();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal * Time.deltaTime * MoveSpeed, 0f, vertical * Time.deltaTime * MoveSpeed);
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
        float yRotation = Input.GetAxis("Rotate");

        Vector3 rotation = new Vector3(0f, yRotation * Time.deltaTime * RotationSpeed, 0f);
        transform.rotation = Quaternion.Euler(rotation + transform.rotation.eulerAngles);
    }
    void Zoom()
    {
        float zoom = Input.GetAxis("Zoom");

        if ((transform.position.y <= MinZoom && zoom > 0) ||
        (transform.position.y >= MaxZoom && zoom < 0))
            zoom = 0;

        transform.Translate(0f, 0f, zoom * Time.deltaTime * ZoomSpeed);
    }
}
