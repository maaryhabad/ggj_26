using UnityEngine;

public class CachoeiraController :MonoBehaviour {
    [Header("Referências")]
    public SpriteRenderer visualCachoeira;
    public ParticleSystem particulasAgua;
    public BoxCollider2D colisorBloqueio;

    [Header("Configurações")]
    public MaskType mascaraQueLibera = MaskType.mTerra; // O usuário sugeriu terra/água
    [Range(0f, 1f)] public float alfaTransparente = 0.3f;

    private void Start() {
        if(GameManager.instance != null) {
            GameManager.instance.cachoeiras.Add(this);
        }
        AtualizarEstado(GameManager.instance.activeMask);
    }

    public void AtualizarEstado(MaskType m) {
        bool podePassar = (m == mascaraQueLibera);

        // 1. Lógica de Colisão: Se pode passar, vira Trigger (atravessa)
        colisorBloqueio.isTrigger = podePassar;

        // 2. Lógica Visual (Sprite)
        if(visualCachoeira != null) {
            Color c = visualCachoeira.color;
            c.a = podePassar ? alfaTransparente : 1f;
            visualCachoeira.color = c;
        }

        // 3. Lógica Visual (Partículas)
        if(particulasAgua != null) {
            var main = particulasAgua.main;
            Color c = main.startColor.color;
            c.a = podePassar ? alfaTransparente : 1f;
            main.startColor = c;
        }
    }
}