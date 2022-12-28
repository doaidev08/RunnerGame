using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI leaderText;
    [SerializeField] Sprite soundOn, soundOff;
    [SerializeField] Image soundImage;

    // Start is called before the first frame update
    void Start()
    {
   
        SetInitSound();
    }

    // Update is called once per frame
    void Update()
    {
        leaderText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();    
    }
    public void ResetAllInfor()
    {
        PlayerPrefs.DeleteAll();
    }
    public void OpenURLFaceBook()
    {
        Application.OpenURL("https://www.facebook.com/doaidev08/");
    }
    private void SetInitSound()
    {
        int soundActive = PlayerPrefs.GetInt("Sound",1);
        if (soundActive==1)
        {
            AudioListener.volume = 1f;
            soundImage.sprite = soundOn;
        }
        else if(soundActive==0)
        {
            AudioListener.volume = 0.0f;
            soundImage.sprite = soundOff;
        }
    }


    public void SoundControl()
    {
        int soundStatusGet = PlayerPrefs.GetInt("Sound");
        if (soundStatusGet==0)
        {
            AudioListener.volume = 1f;
            soundImage.sprite = soundOn;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else if(soundStatusGet == 1)
        {
            AudioListener.volume = 0.0f;
            soundImage.sprite = soundOff;
            PlayerPrefs.SetInt("Sound", 0 );
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
