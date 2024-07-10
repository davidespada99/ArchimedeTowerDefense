using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    
    [Header("References")]
    
    [Header("Attributes")]
    [SerializeField] private float fadeDuration = 1.0f;  // Duration of one fade cycle (in and out)

    
    private Image targetImage;
    private float timer;
    private bool fadingOut = true;

    private bool blink = false;
    

    private void Awake(){
        targetImage = GetComponent<Image>();
    }

    void Update()
    {
        if(blink) BlinkImage();
    }

    private void BlinkImage(){
        timer += Time.unscaledDeltaTime;

        float alpha = fadingOut ? 1 - (timer / fadeDuration) : timer / fadeDuration;

        Color color = targetImage.color;
        color.a = Mathf.Clamp01(alpha);
        targetImage.color = color;

        if (timer >= fadeDuration)
        {
            timer = 0f;
            fadingOut = !fadingOut;  // Reverse the fading direction
        }
    }

    public void ToggleBlink(){
        if( blink == true){
            Color color = targetImage.color;
            color.a = 1f;
            targetImage.color= color;
        }
        blink = !blink;
    }
}
