using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    private void Awake()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        int monitorHeight = Screen.currentResolution.height;
        int monitorWidth = Screen.currentResolution.width;
        
        int windowHeight = Mathf.RoundToInt(monitorHeight * 0.9f);
        int windowWidth = Mathf.RoundToInt(windowHeight * 9f / 16f);
        
        if (windowWidth > monitorWidth * 0.9f)
        {
            windowWidth = Mathf.RoundToInt(monitorWidth * 0.9f);
            windowHeight = Mathf.RoundToInt(windowWidth * 16f / 9f);
        }
        
        Screen.SetResolution(windowWidth, windowHeight, FullScreenMode.Windowed);
        #endif
    }
}
