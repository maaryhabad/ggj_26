using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para trocar de cena

public class GameManager : MonoBehaviour
{
    public void goToPhase1()
    {
        SceneManager.LoadScene("Phase1"); 
    }
}