using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static int Fruit1Count = 0;
    public float remainingTime = 60f; // tempo total do jogo
    private bool _gameEnded = false;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            print("Jogo fechado!");
            Application.Quit();
        }
    }
    
    void Start()
    {
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);  // espera 1 segundo

            remainingTime -= 1f;                  // decrementa 1 segundo

            // Debug log atualizado a cada 1 segundo
            Debug.Log($"Tempo restante: {remainingTime:F0} segundos | gameEnded: {_gameEnded}");
        }

        remainingTime = 0;
        EndGame();
    }
    
    public void EndGame()
    {
        if (!_gameEnded)
        {
            _gameEnded = true;
            Debug.Log("O tempo acabou! Fim de jogo.");
            Application.Quit();
        }
    }
}