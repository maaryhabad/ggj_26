using UnityEngine;
using UnityEngine.UI;

public class MaskUIManager :MonoBehaviour {
    public static MaskUIManager instance;

    [Header("Ícones da UI")]
    public Image iconeNone;
    public Image iconeFogo;
    public Image iconeNuvem;
    public Image iconeTerra;

    [Header("Configurações Visuais")]
    public Color corBloqueado = new Color(0.2f, 0.2f, 0.2f, 0.5f); // Escuro e transparente
    public Color corNormal = Color.white;
    public Color corAtiva = Color.yellow; // Cor para destacar a ativa

    void Awake() {
        instance = this;
    }

    private void Start() {
        AtualizarPainel();
    }

    public void AtualizarPainel() {
        // Pega as referências do GameManager
        var gm = GameManager.instance;

        AtualizarIcone(iconeNone, true, gm.activeMask == MaskType.mNone);

        // Atualiza cada ícone baseado no estado
        AtualizarIcone(iconeFogo, gm.fogoResgatado, gm.activeMask == MaskType.mFogo);
        AtualizarIcone(iconeNuvem, gm.nuvemResgatada, gm.activeMask == MaskType.mNuvem);
        AtualizarIcone(iconeTerra, gm.terraResgatada, gm.activeMask == MaskType.mTerra);
    }

    private void AtualizarIcone(Image img, bool resgatado, bool estaAtiva) {
        if(!resgatado) {
            img.color = corBloqueado; // Máscara que nunca foi pega
            img.transform.localScale = Vector3.one * 0.5f; // Um pouco menor
        } else {
            img.color = corNormal; // Já coletada mas não ativa
            img.transform.localScale = Vector3.one;

            if(estaAtiva) {
                img.color = corAtiva; // Destaque para a ativa
                img.transform.localScale = Vector3.one * 1.2f; // Efeito de "zoom" na ativa
            }
        }
    }
}