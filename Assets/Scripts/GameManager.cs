using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para trocar de cena


public enum MaskType {

    mFogo,
    mNuvem,
    mTerra,
    mAgua,
    mNone

}


public class GameManager :MonoBehaviour {

    public static GameManager instance;


    public AudioManager audioManager;
    public MaskType activeMask = MaskType.mNone;

    // LISTAS DE COISAS DO MAPA
    public List<FogoNuvem> nuvemsDeFogo = new List<FogoNuvem>();
    public List<GeiserAgua> geiseres = new List<GeiserAgua>();
    public List<CachoeiraController> cachoeiras = new List<CachoeiraController>();


    // Flags para saber se já resgataste a máscara
    public bool fogoResgatado = false;
    public bool nuvemResgatada = false;
    public bool terraResgatada = false;
    public bool aguaResgatada = false;




    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }



    public void MudarMascara(MaskType m) {
        activeMask = m; // <--- Importante: Atualiza o estado atual
        AplicarEfeitosVisuais(m); // --- DISPARAR EFEITOS VISUAIS ---

        // Marca como resgatada na primeira vez que usas
        switch(m) {
            case MaskType.mFogo:
                fogoResgatado = true;
                break;
            case MaskType.mNuvem:
                nuvemResgatada = true;
                break;
            case MaskType.mTerra:
                terraResgatada = true;
                break;
            case MaskType.mAgua:
                aguaResgatada = true;
                break;
        }

        // Atualiza a UI (vamos criar este script a seguir)
        MaskUIManager.instance.AtualizarPainel();

        activeMask = m; // ATUALIZA a m�scara ativa
        Debug.Log("Nova Máscara: " + m);

        MudarFogo(m); // Passa a nova m�scara para os objetos
        MudarGeiseres(m);
        MudarCachoeiras(m);

        audioManager.MudarMusica(m);
    }

    private void MudarCachoeiras(MaskType m) {
        // Atualiza todas as cachoeiras do mapa
        cachoeiras.RemoveAll(c => c == null);
        foreach(var c in cachoeiras) {
            c.AtualizarEstado(m);
        }
    }

    private void MudarFogo(MaskType m) {

        // Remove objetos destru�dos da lista para evitar erros
        nuvemsDeFogo.RemoveAll(item => item == null);

        for(int i = 0; i < nuvemsDeFogo.Count; i++) {
            nuvemsDeFogo[i].mudarFogo(m); // Agora sim usa o 'm' correto!
        }

    }

    private void MudarGeiseres(MaskType m) {
        // Limpa refer�ncias nulas caso tenha destru�do algum objeto
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