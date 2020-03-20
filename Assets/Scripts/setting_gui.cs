using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class setting_gui : MonoBehaviour
{
    private bool pauseEnabled = false;
    private bool miniMapShow = true;

    private bool doCountDown = false;
    private float startTime = 0;
    private float countDownTime = 0;

    private GameObject miniMap;
    private GameObject settingMenu;
    private GameObject TimeText;

    void Start()
    {
        pauseEnabled = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
        miniMap = gameObject.transform.GetChild(0).gameObject;
        settingMenu = gameObject.transform.GetChild(1).gameObject;
        TimeText = gameObject.transform.GetChild(2).gameObject;
        //startCountDown(15.0f, demoTime, 10);
        showUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            //check if game is already paused
            if (pauseEnabled == true)
            {
                //unpause the game
                pauseEnabled = false;
                Time.timeScale = 1;
                AudioListener.volume = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                showUI();
            }
            //else if game isn't paused, then pause it
            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                AudioListener.volume = 0;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                showUI();
            }
        }
        if (doCountDown && !pauseEnabled)
        {
            float remainTime = countDownTime - (Time.time - startTime);
            if (remainTime < 0.02)
            {
                doCountDown = false;
                remainTime = 0;
            }
            int mm = (int)remainTime / 60;
            int ss = (int)remainTime % 60;
            int sss = (int)(remainTime * 100) - (mm * 60 + ss) * 100;
            TimeText.GetComponent<Text>().text = mm+":"+ss+"'"+sss;
            if (!doCountDown)
            {
                showUI();
            }
        }
    }
    void showUI()
    {
        if (pauseEnabled)
        {
            if (miniMapShow) settingMenu.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "關閉小地圖";
            else settingMenu.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "開啟小地圖";
        }

        miniMap.SetActive(!pauseEnabled && miniMapShow);
        settingMenu.SetActive(pauseEnabled);
        TimeText.SetActive(!pauseEnabled && doCountDown);
    }
    public void onMiniMapBtn()
    {
        miniMapShow = !miniMapShow;
        showUI();
    }
    public void startCountDown(float time)
    {
        startTime = Time.time;
        countDownTime = time;
        doCountDown = true;
        showUI();
        //method.DynamicInvoke(args);
    }
    private void demoTime(int i)
    {
        Debug.Log(i);
    }
}