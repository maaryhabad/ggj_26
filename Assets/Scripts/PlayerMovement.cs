using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Configurações de Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 10f;

    [Header("Sistema de Máscaras")]
    // 0 = Sem Máscara, 1 = Ar (Pulo)
    public int mascaraAtual = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Chamado pela Action 'move'
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Chamado pela Action 'jump'
    public void OnJump(InputAction.CallbackContext context)
    {
        if (mascaraAtual == 1 && context.started)
        {
            Debug.Log("Botão apertado! Tentando pular...");
        
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            
            rb.AddForce(Vector2.up * 100f, ForceMode2D.Impulse);
        }
    }

    // Chamado pela Action 'SwitchMask' (Tecla E)
    public void OnSwitchMask(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Alterna entre 0 (Sem máscara) e 1 (Ar)
            mascaraAtual = (mascaraAtual == 0) ? 1 : 0;
            
            Debug.Log("Máscara Atual: " + (mascaraAtual == 1 ? "AR" : "NENHUMA"));
            
            // Feedback visual rápido: Muda a cor do player
            GetComponent<SpriteRenderer>().color = (mascaraAtual == 1) ? Color.cyan : Color.white;
        }
    }

    void Update()
    {
        // Movimentação horizontal mantendo a velocidade vertical do pulo/gravidade
        float velocidadeHorizontal = moveInput.x * velocidade;
        //rb.linearVelocity = new Vector2(velocidadeHorizontal, rb.linearVelocity.y);
    }
}