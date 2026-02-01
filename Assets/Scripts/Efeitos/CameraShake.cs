using UnityEngine;
using Unity.Cinemachine; // Use 'using Cinemachine;' se estiver em versões anteriores à Unity 6

public class CameraShake :MonoBehaviour {
    public static CameraShake instance;

    // Referência para o componente que você adicionou
    [SerializeField] private CinemachineImpulseSource impulseSource;

    void Awake() {
        instance = this;

        // Se você esqueceu de arrastar no Inspector, ele tenta pegar sozinho
        if(impulseSource == null) {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }
    }

    public void Tremer(float forca = 1f) {
        if(impulseSource != null) {
            // Gera o tremor baseado nas configurações do componente
            // Você pode multiplicar a força aqui
            impulseSource.GenerateImpulseWithVelocity(Vector3.up * forca);
        }
    }
}