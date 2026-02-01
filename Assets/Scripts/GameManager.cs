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

    public List <FogoNuvem> nuvemsDeFogo = new List<FogoNuvem>();


    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    public void MudarMascara(MaskType m) {
        // 1. Dispara o efeito visual de tela
        if(ScreenEffectManager.instance != null) {
            ScreenEffectManager.instance.TriggerFlash();
        }

        activeMask = m; // ATUALIZA a máscara ativa
        Debug.Log("Nova Máscara: " + m);

        MudarFogo(m); // Passa a nova máscara para os objetos
        audioManager.MudarMusica(m);
    }


    private void MudarFogo(MaskType m) {

        // Remove objetos destruídos da lista para evitar erros
        nuvemsDeFogo.RemoveAll(item => item == null);

        for(int i = 0; i < nuvemsDeFogo.Count; i++) {
            nuvemsDeFogo[i].mudarFogo(m); // Agora sim usa o 'm' correto!
        }

    }




    public void goToPhase1() {
        SceneManager.LoadScene("Phase1");
    }
}