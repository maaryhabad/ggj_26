using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para trocar de cena


public enum MaskType {

    mFogo,
    mNuvem,
    mTerra,
    mNone

}


public class GameManager :MonoBehaviour {



    public static GameManager instance;


    public AudioManager audioManager;



    private MaskType activeMask = MaskType.mNone;

    // private bool mostrarFogo = true; // true = fogo / fase = nuvem

    public List<FogoNuvem> nuvemsDeFogo = new List<FogoNuvem>();
    public List<GeiserAgua> geiseres = new List<GeiserAgua>();


    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    public void MudarMascara(MaskType m) {
        // --- DISPARAR EFEITOS VISUAIS ---
        AplicarEfeitosVisuais(m);
        // -------------------------------

        activeMask = m; // ATUALIZA a máscara ativa
        Debug.Log("Nova Máscara: " + m);

        MudarFogo(m); // Passa a nova máscara para os objetos
        MudarGeiseres(m);

        audioManager.MudarMusica(m);
    }


    private void MudarFogo(MaskType m) {

        // Remove objetos destruídos da lista para evitar erros
        nuvemsDeFogo.RemoveAll(item => item == null);

        for(int i = 0; i < nuvemsDeFogo.Count; i++) {
            nuvemsDeFogo[i].mudarFogo(m); // Agora sim usa o 'm' correto!
        }

    }

    private void MudarGeiseres(MaskType m) {
        // Limpa referências nulas caso tenha destruído algum objeto
        geiseres.RemoveAll(g => g == null);

        foreach(GeiserAgua g in geiseres) {
            g.AtualizarEstado(m);
        }
    }

    private void AplicarEfeitosVisuais(MaskType m) {
        // 1. Define a cor do flash baseado na máscara
        Color corDoImpacto = Color.white; // Cor padrão
        float forcaTremida = 0.2f; // Tremida padrão

        switch(m) {
            case MaskType.mFogo:
                // Laranja/Vermelho vivo para fogo
                ColorUtility.TryParseHtmlString("#FF4500", out corDoImpacto);
                forcaTremida = 0.4f; // Fogo treme mais forte!
                break;
            case MaskType.mNuvem:
                // Azul ciano claro para vento
                ColorUtility.TryParseHtmlString("#00FFFF", out corDoImpacto);
                forcaTremida = 0.15f; // Vento treme mais suave
                break;
            case MaskType.mTerra:
                // Marrom/Laranja escuro
                ColorUtility.TryParseHtmlString("#8B4513", out corDoImpacto);
                forcaTremida = 0.5f; // Terra treme pesado!
                break;
            default:
                // Um cinza para "sem máscara"
                corDoImpacto = Color.gray;
                break;
        }

        // 2. Chama o Flash Colorido (se o script existir)
        if(ScreenEffectManager.instance != null) {
            // Usei um alpha de 0.7f para não tapar 100% a visão, fica mais estiloso
            corDoImpacto.a = 0.7f;
            ScreenEffectManager.instance.TriggerColoredFlash(corDoImpacto);
        }

        if(CameraShake.instance != null) {
            // Agora o valor é apenas um multiplicador de força
            float intensidade = (m == MaskType.mTerra) ? 1.5f : 0.5f;
            CameraShake.instance.Tremer(intensidade);
        }

    }



    public void goToPhase1() {
        SceneManager.LoadScene("Phase1");
    }
}