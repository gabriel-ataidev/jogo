using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            print("Jogo fechado!");
            Application.Quit();
        }
    }
}