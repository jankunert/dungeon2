using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintingSpeed = 3.0f;
    public float sneakSpeed = 2.0f;
    private bool sprinting=false;
    private bool sneaking = false;
    private float rotationChangingSpeed = 10000.0f;
    public float rotationSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Quaternion destRotate;
    private bool pause;
    private CharacterController controller;
    private Quaternion bordermax;
    private Quaternion bordermin;
    public AudioClip aud;
    public float steplength = 0.4f;
    public float delay = 0;
    public float volume = 0.7f;
    public Text pauseText;
    public Text resumeText;
    public Button exit;
    public GameObject MainCamera;
    public GameObject gameController;

    public Text rota;
    private float rotaF;
    private string rotaS;
    // Use this for initialization
    void Start()
    {

        

        pauseText.enabled = false;
        exit.gameObject.SetActive(false);
        resumeText.enabled = false;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        destRotate = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        rotaF = transform.localEulerAngles.x;
        rotaS = rotaF.ToString();
        rota.text = rotaS;



        Sprint ();
        Sneak();
        if (pause == false)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (sprinting)
            {
                moveDirection = moveDirection * (speed + sprintingSpeed);
            }
            else if (sneaking)
            {
                moveDirection = moveDirection * (speed / sneakSpeed);
            }
            else
            {
                moveDirection = moveDirection * speed;
            }

            moveDirection = transform.TransformDirection(moveDirection);
            controller.SimpleMove(moveDirection);
            if ((rotaF >= 0 && rotaF <= 80) || 280 <= rotaF && rotaF <= 360)
            { 

                destRotate.eulerAngles -= new Vector3(Input.GetAxis("MY") * rotationSpeed, 0, 0);
            }
            else if(rotaF>80&&rotaF<120)
            {
                destRotate.eulerAngles -= new Vector3(3, 0, 0);
            }
            else if(rotaF>200&&rotaF<280)
            {
                destRotate.eulerAngles += new Vector3(3, 0, 0);
            }
                destRotate.eulerAngles += new Vector3(0, Input.GetAxis("MouseX") * rotationSpeed, 0);


            /*if (Input.GetKey(KeyCode.Q))
            {
                destRotate.eulerAngles -= new Vector3(0, rotationSpeed, 0);

            }
            if (Input.GetKey(KeyCode.E))
            {
                destRotate.eulerAngles += new Vector3(0, rotationSpeed, 0);


            }*/




            float step = rotationChangingSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRotate, step);


            if (controller.velocity.sqrMagnitude > 0.2f)
            {
                if (delay > steplength)
                {
                    AudioSource.PlayClipAtPoint(aud, transform.position, volume);

                    if (sprinting == false) delay = 0;
                    if (sprinting) delay = 0.1f;

                }
            }
            delay += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
                pauseText.enabled = true;
                exit.gameObject.SetActive(true);
                resumeText.enabled = true;
                pause = true;
                Time.timeScale = 0.0f;
                //gameController.GetComponent<AudioSource>.pause
            }
            
        }
        else if (pause == true && Input.GetKeyDown(KeyCode.Escape))
        {

            Resume();

        }


    }
    private void Sprint()
    {
        if (pause == false)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sprinting = true;
                MainCamera.transform.position += new Vector3(0,0.05F,0);
                AudioSource.PlayClipAtPoint(aud, transform.position, volume);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {

                sprinting = false;
                MainCamera.transform.position += new Vector3(0, -0.05F, 0);
                delay = 0;
            }
        }
    }
    private void Sneak()
    {
        if (pause == false)
        {

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                sneaking = true;
                MainCamera.transform.position += new Vector3(0, -0.1F, 0);
                AudioSource.PlayClipAtPoint(aud, transform.position, volume);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {

                sneaking = false;
                MainCamera.transform.position += new Vector3(0, +0.1F, 0);
                delay = 0;
            }
        }
    }
    public void Resume()
    {

        Time.timeScale = 1.0f;
        Cursor.visible = false;
        pause = false;
        pauseText.enabled = false;
        exit.gameObject.SetActive(false);
        resumeText.enabled = false;
        
    }
    

}