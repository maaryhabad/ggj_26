using UnityEngine;
using System.Collections;

public class ScreenEffectManager :MonoBehaviour {
    public static ScreenEffectManager instance;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duracaoFlash = 0.2f;

    void Awake() {
        instance = this;
    }

    public void TriggerFlash() {
        StopAllCoroutines();
        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect() {
        // Sobe o alpha rápido (esconde a troca)
        canvasGroup.alpha = 1f;

        float tempo = 0;
        while(tempo < duracaoFlash) {
            tempo += Time.deltaTime;
            // Desce o alpha gradualmente
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, tempo / duracaoFlash);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}