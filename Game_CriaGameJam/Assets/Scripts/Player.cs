using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _Speed = 5f;
    private Rigidbody2D _Player;
    private Vector2 _PlayerDIR;   // Eixos X e Y
    private Animator PlayerAnim;

    private float _runSpeed = 15f;
    private float runDuration = 10f;
    private float runTimeLeft;
    private bool isRunning = false;


    private const int MOVIMENTO_ANDANDO = 1;
    private const int MOVIMENTO_PARADO = 0;

    void Start()
    {
        PlayerAnim = GetComponent<Animator>(); // Pega o animator do inspector
        _Player = GetComponent<Rigidbody2D>();  // Pega o Rigidbody2D do inspector
    }

    void Update()
    {
        _PlayerDIR = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_PlayerDIR.sqrMagnitude > 0) // Verifica se está em movimento
        {
            PlayerAnim.SetInteger("Movimento", MOVIMENTO_ANDANDO);
        }
        else
        {
            PlayerAnim.SetInteger("Movimento", MOVIMENTO_PARADO);
        }

        flip();

        // Checar se o jogador pressiona o botão Shift para começar a correr
        if (Input.GetButtonDown("Fire1") && !isRunning)
        {
            isRunning = true;
            _Speed = _runSpeed; // Aumenta a velocidade
            runTimeLeft = runDuration; // Define o tempo que o personagem vai correr
        }

        // Se o personagem estiver correndo, diminuir o tempo restante
        if (isRunning)
        {
            runTimeLeft -= Time.deltaTime;

            // Quando o tempo de corrida acabar, voltar à velocidade normal
            if (runTimeLeft <= 0)
            {
                _Speed = 5f; // Volta para a velocidade normal
                isRunning = false;
            }
        }
    }

    void FixedUpdate() // Chamado em intervalos fixos para física
    {
        _Player.MovePosition(_Player.position + _PlayerDIR * _Speed * Time.fixedDeltaTime); // Movimenta o jogador
    }

    void flip() // Inverte o sprite com base na direção
    {
        if (_PlayerDIR.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f); // Direção normal
        }
        else if (_PlayerDIR.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f); // Inverte o sprite
        }
    }
}
