using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {
    public float speed = 5.0f;
    public float rotationChangingSpeed = 200.0f;
    public float rotationSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Quaternion destRotate;
    private CharacterController controller;

    public AudioClip aud;
    public float steplength = 0.4f;
    public float delay = 0;
    public float volume = 0.7f;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        destRotate = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = moveDirection * speed;

        moveDirection = transform.TransformDirection(moveDirection);
        controller.SimpleMove(moveDirection);
        if (Input.GetKey(KeyCode.Q))
        {
            destRotate.eulerAngles -= new Vector3(0, rotationSpeed, 0);

        }
        if (Input.GetKey(KeyCode.E))
        {
            destRotate.eulerAngles += new Vector3(0, rotationSpeed, 0);


        }
        float step = rotationChangingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, destRotate, step);


        if (controller.velocity.sqrMagnitude >0.2f)
        {
            if (delay>steplength)
            {
                AudioSource.PlayClipAtPoint(aud, transform.position, volume);
                delay = 0;

            }
        }
        delay += Time.deltaTime;
    }
}
