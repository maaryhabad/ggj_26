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


    public void MudarFogo(MaskType m) {

        // mostrarFogo = fogo;

        for(int i = 0; i < nuvemsDeFogo.Count; i++) {
            nuvemsDeFogo[i].mudarFogo(activeMask);
        }

    }




    public void goToPhase1() {
        SceneManager.LoadScene("Phase1");
    }
}