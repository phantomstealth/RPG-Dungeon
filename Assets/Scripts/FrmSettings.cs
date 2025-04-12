using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrmSettings : MonoBehaviour
{
    public GameObject JCanvas1;
    public GameObject JCanvas2;
    public GameObject JCanvas3;

    public GameObject btnSettings;
    private GameObject HUD;
    public GameObject formSettings;
    public GameObject formMenu;
    public Slider musSlider;
    public Slider camSlider;

    public Text txtResolution;
    public Text txtHeightOrtoghapicCamera;

    private MusicContoller musController;

    private bool beginStart;
    // Start is called before the first frame update

    private void FirstStart()
    {
        if (beginStart) return;
        beginStart = true;
        txtResolution.text = "Resolution:" + Screen.width.ToString() + "X" + Screen.height.ToString();
        txtHeightOrtoghapicCamera.text = "Height Ortographic Camera: " + Camera.main.orthographicSize.ToString();
        musController = FindObjectOfType<MusicContoller>();
        HUD = GameObject.FindGameObjectWithTag("HUD");
        LoadSettings();
    }

    public void BtnResume()
    {
        musController.SwitchTrack(0);
        Time.timeScale = 1f;
        JCanvas1.SetActive(true);
        JCanvas2.SetActive(true);
        JCanvas3.SetActive(true);
        btnSettings.SetActive(true);
        HUD.gameObject.SetActive(true);
        formMenu.SetActive(false);
        formSettings.SetActive(false);
    }

    public void BtnActive_Settings()
    {
        FirstStart();

        musController.SwitchTrack(2);
        Time.timeScale = 0f;
        formSettings.SetActive(true);
        HUD.gameObject.SetActive(false);
        JCanvas1.SetActive(false);
        JCanvas2.SetActive(false);
        JCanvas3.SetActive(false);
        btnSettings.SetActive(false);
    }

    public void BtnQuit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void BtnMenu()
    {
        formMenu.SetActive(true);
        formSettings.SetActive(false);
    }

    private float PlayerPrefsSave(string TextParameter, float fValue,float fDefault)
    {
        if (PlayerPrefs.HasKey(TextParameter))
        {
            return PlayerPrefs.GetFloat(TextParameter);
        }
        else
        {
            PlayerPrefs.SetFloat(TextParameter, fValue);
            return fDefault;
        }
    }

    public void LoadSettings()
    {
        camSlider.value = PlayerPrefsSave("HeightCamera", camSlider.value,5f);
        musSlider.value = PlayerPrefsSave("MusicVolume", musSlider.value,1f);

        musController.curVolume = musSlider.value;
        Camera.main.orthographicSize = (camSlider.value);
    }

    public void ChangeVolumeMusic()
    {
        musController.ChangeVolumeMusic(musSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musSlider.value);
    }

    public void ChangeHeightCamera()
    {
        Camera.main.orthographicSize=(camSlider.value);
        PlayerPrefs.SetFloat("HeightCamera", camSlider.value);
        txtResolution.text = "Resolution:" + Screen.width.ToString() + "X" + Screen.height.ToString();
        txtHeightOrtoghapicCamera.text = "Height Ortographic Camera: " + Camera.main.orthographicSize.ToString();
    }


    public void BtnSave()
    {
    }

    public void BtnLoad()
    {
    }


    }
