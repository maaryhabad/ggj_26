using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement :MonoBehaviour {

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Configurações de Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 10f;

    [Header("Sistema de Máscaras")]
    public int mascaraAtual = 0; // 1 = Ar/Nuvem

    [Header("Controle de Pulo")]
    [SerializeField] private int pulosEfetuados = 0;
    [SerializeField] private int maxPulosAr = 2;
    [SerializeField] private bool estaNoChao;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update() {
        float velocidadeHorizontal = moveInput.x * velocidade;
        rb.linearVelocity = new Vector2(velocidadeHorizontal, rb.linearVelocity.y);

        float move = moveInput.x; // valor do seu input (-1 a 1)

        if (move > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        } 
        else if (move < 0) {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
    }

    // --- DETECÇÃO DE CHÃO ---
    // Reseta os pulos ao tocar em algo com a Tag "Chao"
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Chao")) {
            estaNoChao = true;
            pulosEfetuados = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Chao")) {
            estaNoChao = false;
        }
    }

    // --- DETECÇÃO DE MÁSCARAS (Trigger/Atravessa) ---
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Atravessei algo: " + other.gameObject.name);

        if(other.CompareTag("mascara")) {
            NumeroMascara scriptMascara = other.GetComponent<NumeroMascara>();

            if(scriptMascara != null) {
                MudarMascara(scriptMascara.numeroMascara);
                Debug.Log("Máscara coletada! Número: " + scriptMascara.numeroMascara);
            } else {
                Debug.LogError("O objeto tem a tag 'mascara' mas falta o script 'NumeroMascara'!");
            }
        }
    }


    // --- MUDANÇA DE MÁSCARA ---
    private void MudarMascara(int numeroMascara) {
        mascaraAtual = numeroMascara;
        Debug.Log("Alterando Mascara para " + numeroMascara);

        // Resetamos os pulos ao trocar de máscara para evitar bugs
        pulosEfetuados = 0;

        switch(mascaraAtual) {
            case 1:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                GameManager.instance.MudarMascara(MaskType.mNuvem);
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.red;
                GameManager.instance.MudarMascara(MaskType.mFogo);
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.brown;
                GameManager.instance.MudarMascara(MaskType.mTerra);
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.white;
                GameManager.instance.MudarMascara(MaskType.mNone);
                break;
        }
    }



    // Chamado pela Action 'move'
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    // --- LÓGICA DE PULO ---
    // Chamado pela Action 'jump'
    public void OnJump(InputAction.CallbackContext context) {
        // 1. Se NÃO for a máscara de ar (1), ignora o comando de pulo completamente
        if(mascaraAtual != 1)
            return;

        // 2. Só executa no frame em que o botão é pressionado
        if(context.started) {
            // 3. Verifica se ainda tem pulos disponíveis (máximo de 2 para o Ar)
            if(pulosEfetuados < maxPulosAr) {
                ExecutarPulo();
            }
        }
    }

    private void ExecutarPulo() {
        pulosEfetuados++;

        // Zera a velocidade vertical para o segundo pulo ter a mesma força que o primeiro
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);

        Debug.Log($"Pulo {pulosEfetuados}/{maxPulosAr} executado!");
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


   
}