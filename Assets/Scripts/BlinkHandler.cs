using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkHandler : MonoBehaviour
{
    public static BlinkHandler instance;

    [Header("References")]
    [SerializeField] private GameObject[] imageGameObjects;

    private Blink[] blinkScripts;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of BlinkHandler
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Optional: to keep the instance across scenes
        }
    
      
    }

    private void Start()
    {
        // Initialize the Blink scripts array
        blinkScripts = new Blink[imageGameObjects.Length];

        // Get the Blink component from each GameObject and store it in the array
        for (int i = 0; i < imageGameObjects.Length; i++)
        {
            if (imageGameObjects[i] != null)
            {
                blinkScripts[i] = imageGameObjects[i].GetComponent<Blink>();
                if (blinkScripts[i] == null)
                {
                    Debug.LogError("No Blink component found on GameObject at index " + i);
                }
            }
            else
            {
                Debug.LogError("Null GameObject at index " + i);
            }
        }
    }

    public void ToggleBlink(int imageIndex)
    {
        Debug.Log("Toggle Blink on imageIndex " + imageIndex);
        if (imageIndex >= 0 && imageIndex < blinkScripts.Length)
        {
            if (blinkScripts[imageIndex] != null)
            {
                blinkScripts[imageIndex].ToggleBlink();
            }
            else
            {
                Debug.LogError("Blink script not found on GameObject at index " + imageIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid image index: " + imageIndex);
        }
    }
}
