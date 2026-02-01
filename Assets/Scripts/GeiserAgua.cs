using UnityEngine;
using System.Collections.Generic;

public class GeiserAgua :MonoBehaviour {
    [Header("Visual e Física")]
    public ParticleSystem minhasParticulas; // Referência ao sistema de partículas
    public Collider2D meuCollider; // O trigger invisível que empurra

    [Header("Configurações de Impulso")]
    public float forcaLevitacao = 25f;
    public float velocidadeMaximaSubida = 8f;

    private bool ativo = false;

    void Start() {
        if(GameManager.instance != null) {
            // Se certifica de que não está duplicado na lista
            if(!GameManager.instance.geiseres.Contains(this)) {
                GameManager.instance.geiseres.Add(this);
            }
        }
        // Começa desligado (ou chama o estado inicial correto)
        AtualizarEstado(MaskType.mNone);
    }

    public void AtualizarEstado(MaskType m) {
        ativo = (m == MaskType.mTerra);

        // --- CONTROLE DAS PARTÍCULAS ---
        // Acessamos o módulo de emissão para ligar/desligar o fluxo
        var emissionModule = minhasParticulas.emission;
        emissionModule.enabled = ativo;

        // --- CONTROLE DA FÍSICA ---
        // O collider só existe se a máscara estiver ativa
        meuCollider.enabled = ativo;
    }

    // A lógica de física permanece idêntica, pois funciona muito bem!
    private void OnTriggerStay2D(Collider2D other) {
        if(!ativo || meuCollider == null || !meuCollider.enabled)
            return;

        if(other.CompareTag("Player")) {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if(rb != null) {
                if(rb.linearVelocity.y < velocidadeMaximaSubida) {
                    // Usa ForceMode2D.Force para um empurrão constante e suave
                    rb.AddForce(Vector2.up * forcaLevitacao, ForceMode2D.Force);
                }
            }
        }
    }
}