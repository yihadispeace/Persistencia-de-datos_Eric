using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animatronix;
    public Transform cam;
    public Transform LookAtTransform;
    


    //variables salto y movimiento
    public float speed = 5;
    public float jumpHeight = 1;
    public float gravity = -9.81f;

    //variables para el ground sensor
    public bool isGrounded;
    public Transform groundSensor;
    public float sensorRadius = 0.1f;
    public LayerMask ground;
    private Vector3 playerVelocity;

    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;


    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;    

 void Start()
    {
        //Asignar componentes a las variables
        controller = GetComponent<CharacterController>();
        animatronix = GetComponentInChildren<Animator>();
        
        //Función para que el ratón desaparezca
        Cursor.lockState = CursorLockMode.Locked;
	
    }

 void Update()
    {
                
        Movement();
        Jump(); 

    }

void Movement()
    {
        
        //Asignar los inputs de los ejes a las variables
        float z = Input.GetAxisRaw("Vertical");        
        float x = Input.GetAxisRaw("Horizontal");   
        animatronix.SetFloat("VelX", x);
        animatronix.SetFloat("VelZ", z);

        //El vector del movimiento que usa el eje X y Z(hacía los lados y hacía delante)
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        //Si el vector de movimiento es diferente del vector sin movimiento
        if(move != Vector3.zero)
        {
           
            
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;           
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);           
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;            
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
       }

void Jump()
    {
        
        
       
        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, ground);
        animatronix.SetBool("Jump", !isGrounded);     

        
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        
        /*Si estamos en el suelo y le damos al botón de saltar, la velocidad del personaje en el eje vertical será la raíz cuadrada de la altura de salto por la gravedadd 
        y un número negativo para contrarrestar la gravedad y que la raíz cuadrada */
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
          
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
        }

        
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
     
    
}
