using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public static bool[] isOn = {true, true};

    public GameObject handle;
    private RectTransform handleTransform;
    private float handleSize;
    public RectTransform toggle;
    private float onPosX;
    private float offPosX;
    public float handleOffset;

    public GameObject onIcon;
    public GameObject offIcon;

    public float moveSpeed;
    public float t = 0.0f;
    private bool switching = false;

    [SerializeField] private int settingID;


    public void Awake()
    {
        
        handleTransform = handle.GetComponent<RectTransform>();
        
        handleSize = handleTransform.sizeDelta.x;
        float toggleSizeX = toggle.sizeDelta.x;
        onPosX = (toggleSizeX / 2) - (handleSize / 2);
        offPosX = onPosX * handleOffset ;
    }
    // Start is called before the first frame update

    void Start()
    {
         

        

        if (isOn[settingID])
        {
            handleTransform.localPosition = new Vector3(onPosX, 0, 0);
            onIcon.gameObject.SetActive(true);
            offIcon.gameObject.SetActive(false);
        }
        else
        {
            handleTransform.localPosition = new Vector3(offPosX, 0, 0);
            onIcon.gameObject.SetActive(false);
            offIcon.gameObject.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("For SettingID " + settingID+ ": isOn: "+ isOn);
        if (switching)
        {
            StartToggleing(isOn[settingID]);
        }

    }

    private void StartToggleing(bool tStatus)
    {
        if (!onIcon.activeSelf || !offIcon.activeSelf)
        {
            onIcon.gameObject.SetActive(true);
            offIcon.gameObject.SetActive(false);
        }
        if (tStatus)
        {
            handleTransform.localPosition = SmoothlyMove(handle, onPosX, offPosX);
            onIcon.gameObject.SetActive(false);
            offIcon.gameObject.SetActive(true);
        }
        else
        {
            handleTransform.localPosition = SmoothlyMove(handle, offPosX, onPosX);
            onIcon.gameObject.SetActive(true);
            offIcon.gameObject.SetActive(false);
        }
    }

    private Vector3 SmoothlyMove(GameObject handle, float startPosX, float endPosX)
    {
        Vector3 position = new Vector3(Mathf.Lerp(startPosX, endPosX, t += moveSpeed * Time.deltaTime), 0, 0);
        StopSwitching();
        return position;
    }

    void StopSwitching()
    {
        if (t > 1.0f)
        {
            switching = false;
            t = 0.0f;
            switch (isOn[settingID])
            {
                case true: 
                    isOn[settingID] = false;
                    //sound
                    if(settingID == 0) {
                        Debug.Log("sound off");
                        SoundManager.Instance.PauseSound();
                    }
                    //hand
                    if(settingID == 1){
                        ScenesManager.swapped = true;
                        Debug.Log("ScenesManager.swapped" + ScenesManager.swapped);
                    }
                    break;
                case false: 
                    isOn[settingID] = true;
                    //sound
                    if(settingID == 0) {
                        Debug.Log("sound on");
                        SoundManager.Instance.PlaySound();
                    }
                    //hand
                    if(settingID == 1){
                        ScenesManager.swapped = false;
                        Debug.Log("ScenesManager.swapped" + ScenesManager.swapped);
                    }
                    break;
            }
        }
    }
    public void Switch()
    {
        switching = true;
    }
}
