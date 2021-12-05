using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    public Rigidbody rb;
    public Vector3 lastPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    
    }

    // Update is called once per frame
    void Update()
    {
        var transform = this.GetComponent<Transform>();
        var velocity = rb.velocity;
        Vector3 obj_velocity = (lastPosition - transform.position) * Time.deltaTime;
        lastPosition = transform.position;
        rb.velocity = obj_velocity;
        var gamma = Input.GetAxis("Horizontal");
        //transform.localRotation *= Quaternion.Euler(0.0f, gamma * 50.0f * Time.deltaTime, 0.0f);
        var delta = Input.GetAxis("Vertical") * this.speed * transform.forward;
        this.GetComponent<CharacterController>().SimpleMove(delta);
        if (delta.magnitude > 0.1f){
            if (gamma != 0){
                transform.localRotation *= Quaternion.Euler(0.0f, gamma * this.speed * Time.deltaTime * 4, 0.0f);
            }
            if (speed < 10f){
                speed += .01f;
            }
        }
        else if (delta.magnitude < 0.1f && speed > 1f && rb.velocity.z < 0f){
            this.GetComponent<CharacterController>().SimpleMove(this.speed * transform.forward);
            speed -= .01f;
        }
        else if (delta.magnitude < 0.1f && speed > 1f && rb.velocity.z > 0f){
            this.GetComponent<CharacterController>().SimpleMove(this.speed * transform.forward * -1);
            speed -= .01f;
        }
    }
}
