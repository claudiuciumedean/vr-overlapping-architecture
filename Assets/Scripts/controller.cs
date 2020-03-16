using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float speed = 3f;
    public GameObject camera;
    Rigidbody rb;
    Vector2 input;
    float horizontalSpeed = 2.0f;
    float verticalSpeed = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // vertical rotation for camera
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        float z = camera.transform.eulerAngles.z;

        camera.transform.Rotate(-v, 0, -z);

        // horizontal rotation for the body
        float h = horizontalSpeed * Input.GetAxis("Mouse X");

        transform.Rotate(0, h, 0);

        // moving the body with keyboard input
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input,1);

        Vector3 forward = transform.forward; 
        Vector3 right = transform.right; 

        transform.position += (forward.normalized * input.y + right.normalized * input.x) * Time.deltaTime * 5;
    }
}