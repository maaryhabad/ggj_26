using UnityEngine;

public class FogoNuvem : MonoBehaviour
{
    public Sprite fogo, nuvem;
    public SpriteRenderer minhaSprite;


    [Header("Configurações")]
    public float velocidade = 4f;
    public float forcaTrampolim = 12f;

    // Vari�vel para controlar se ele deve se mover
    private MaskType estadoAtual = MaskType.mNone;
    private Camera cam;

    void Start() {
        // Se registra no GameManager automaticamente
        if(GameManager.instance != null) {
            GameManager.instance.nuvemsDeFogo.Add(this);
        }

        cam = Camera.main;
        mudarFogo(MaskType.mNuvem);
    }

    void Update() {
        // Só SE MOVE SE O ESTADO FOR FOGO
        if(estadoAtual == MaskType.mFogo) {
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
        }

        // 2. Verificação de limite da câmera
        VerificarSaidaPelaEsquerda();
    }

    public void mudarFogo(MaskType m) {
        
        estadoAtual = m;

        switch(m) {
            case MaskType.mFogo:
                minhaSprite.enabled = true; // Visível
                minhaSprite.sprite = fogo;
                gameObject.tag = "Dano"; // Vira perigo
                break;

            case MaskType.mNuvem:
                minhaSprite.enabled = true; // Visível
                minhaSprite.sprite = nuvem;
                gameObject.tag = "Trampolim"; // Vira plataforma
                break;

            default:
                minhaSprite.enabled = false; // INVisível
                gameObject.tag = "Dano"; // Ainda dá dano!
                break;
        }
    }

    private void VerificarSaidaPelaEsquerda() {
        // Converte a posição do mundo para a visão da câmera (0 a 1)
        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

        // Se o X for menor que 0, ele saiu pela esquerda
        // Usamos -0.2f como uma margem de segurança para o objeto sumir totalmente
        if(viewportPos.x < -0.2f) {
            // Antes de destruir, removemos da lista do GameManager para evitar erros de referência nula
            if(GameManager.instance != null) {
                GameManager.instance.nuvemsDeFogo.Remove(this);
            }
            Destroy(gameObject);
        }
    }

}
