using UnityEngine;
using UnityEngine.UI;

public class MaskUIManager :MonoBehaviour {
    public static MaskUIManager instance;

    [Header("Ícones da UI")]
    public Image iconeNone;
    public Image iconeFogo;
    public Image iconeNuvem;
    public Image iconeTerra;
    public Image iconeAgua;

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
        AtualizarIcone(iconeAgua, gm.terraResgatada, gm.activeMask == MaskType.mAgua);
    }

    private void AtualizarIcone(Image img, bool resgatado, bool estaAtiva) {
        if(img == null)
            return;

        if(!resgatado) {
            // Caso a máscara ainda não tenha sido coletada
            img.color = corBloqueado;
            img.transform.localScale = Vector3.one * 0.4f; // Fica pequena
        } else {
            // Caso a máscara já tenha sido coletada (ou seja o ícone X)
            img.color = estaAtiva ? corAtiva : corNormal;

            // AJUSTE AQUI: 
            // Se estiver ativa, escala 1.2. Se não estiver, escala 0.8 (pequena)
            img.transform.localScale = estaAtiva ? Vector3.one * 1.0f : Vector3.one * 0.3f;
        }
    }
}