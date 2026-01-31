using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement :MonoBehaviour {
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("Configurações de Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 10f;

    [Header("Sistema de Máscaras")]
    // 0 = Sem Máscara, 1 = Ar (Pulo)
    public int mascaraAtual = 0;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        Debug.Log("A");

        if(collision.gameObject.tag == "mascara") {

            int numeroMascara = collision.gameObject.GetComponent<NumeroMascara>().numeroMascara;
            MudarMascara(numeroMascara);

        }
    }


    private void MudarMascara(int numeroMascara) {
        // Alterna entre 0 (Sem máscara) e 1 (Ar)
        // mascaraAtual = (mascaraAtual == 0) ? 1 : 0;
        mascaraAtual = numeroMascara;
        Debug.Log("Alterando Mascara para" + numeroMascara);

        // Feedback visual rápido: Muda a cor do player
        // Debug.Log("Máscara Atual: " + (mascaraAtual == 1 ? "AR" : "NENHUMA"));

        switch(mascaraAtual) {
            case 1:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                Debug.Log("Máscara Atual: " + "AR");
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log("Máscara Atual: " + "FOGO");
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.blue;
                Debug.Log("Máscara Atual: " + "");
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.white;
                Debug.Log("Máscara Atual: " + "NENHUMA");
                break;
        }

    }



    // Chamado pela Action 'move'
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    // Chamado pela Action 'jump'
    public void OnJump(InputAction.CallbackContext context) {
        if(mascaraAtual == 1 && context.started) {
            Debug.Log("Botão apertado! Tentando pular...");

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
        }
    }

    // Chamado pela Action 'SwitchMask' (Tecla E)
    public void OnSwitchMask1(InputAction.CallbackContext context) {
        if(context.performed) {
            MudarMascara(1);
        }
    }

    public void OnSwitchMask2(InputAction.CallbackContext context) {
        if(context.performed) {
            MudarMascara(2);
        }
    }

    public void OnSwitchMask3(InputAction.CallbackContext context) {
        if(context.performed) {
            MudarMascara(3);
        }
    }

    public void OnSwitchMask4(InputAction.CallbackContext context) {
        if(context.performed) {
            MudarMascara(4);
        }
    }


    void Update() {
        // Movimentação horizontal mantendo a velocidade vertical do pulo/gravidade
        float velocidadeHorizontal = moveInput.x * velocidade;
        rb.linearVelocity = new Vector2(velocidadeHorizontal, rb.linearVelocity.y);
    }
}