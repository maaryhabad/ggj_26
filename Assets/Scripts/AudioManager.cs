using UnityEngine;
using UnityEngine.Audio;

public class AudioManager :MonoBehaviour {


    [SerializeField]
    private AudioSource audioSource;
    // public AudioMixerSnapshot effectMood;

    [SerializeField]
    private AudioClip musicaNormal, musicaFogo, musicaVento, musicaTerra;

    private void Start() {
        // effectMood.TransitionTo(0.5f);
    }

    public void MudarMusica(MaskType m) {

        AudioClip newClip;

        if(audioSource!= null) {

            // 1. Salva a posição exata da amostra atual
            int currentSamples = audioSource.timeSamples;

            switch(m) {
                case MaskType.mFogo:
                    newClip = musicaFogo;
                    break;
                case MaskType.mNuvem:
                    newClip = musicaVento;
                    break;
                case MaskType.mTerra:
                    newClip = musicaTerra;
                    break;
                case MaskType.mNone:
                    newClip = musicaNormal;
                    break;
                default:
                    newClip = musicaNormal;
                    break;
            }

            // 2. Troca o clipe
            audioSource.clip = newClip;

            // 3. Ajusta a posição no novo clipe 
            // (O modulo '%' garante que não dê erro se o novo clipe for levemente menor)
            audioSource.timeSamples = currentSamples % newClip.samples;


            audioSource.Play();

        }

    }


}
