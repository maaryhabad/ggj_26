using UnityEngine;
using UnityEngine.UI; // Necessário para acessar o componente Image
using System.Collections;

public class ScreenEffectManager :MonoBehaviour {
    public static ScreenEffectManager instance;

    [Header("Referências UI")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image overlayImage; // Referência à Imagem para mudar a cor

    [Header("Configurações")]
    [SerializeField] private float duracaoFadeIn = 0.05f; // Entrada muito rápida
    [SerializeField] private float duracaoFadeOut = 0.3f; // Saída mais suave

    void Awake() {
        instance = this;
        // Garante que começa invisível
        if(canvasGroup)
            canvasGroup.alpha = 0f;
    }

    // Agora o método aceita uma COR
    public void TriggerColoredFlash(Color corDoFlash) {
        if(overlayImage != null) {
            overlayImage.color = corDoFlash;
        }
        StopAllCoroutines();
        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect() {
        // 1. Fade In Rápido (Aparece a cor)
        float tempo = 0;
        while(tempo < duracaoFadeIn) {
            tempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, tempo / duracaoFadeIn);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 2. Fade Out Suave (A cor some)
        tempo = 0;
        while(tempo < duracaoFadeOut) {
            tempo += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, tempo / duracaoFadeOut);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}