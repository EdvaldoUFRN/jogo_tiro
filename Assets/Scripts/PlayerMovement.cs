
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //resetando a velocidade padrão
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //pegando as entradas
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //criando o vetor de movimento
        Vector3 move = transform.right * x + transform.forward * z;

        //aplicando o movimento ao jogador
        controller.Move(move * speed * Time.deltaTime);

        //verificar se o jogador pode pular
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //pulando de fato
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //caindo
        velocity.y += gravity * Time.deltaTime;

        //pulando
        controller.Move(velocity * Time.deltaTime);

        isMoving = lastPosition != transform.position && isGrounded;

        lastPosition = gameObject.transform.position;

    }
}
