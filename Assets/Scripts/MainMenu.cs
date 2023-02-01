using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private Button playButton;
    
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDurationMinutes = 1;
    
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private int energy;

    public void Start()
    {
        OnApplicationFocus(true);
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) return;
        
        CancelInvoke();
        
        int highscore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        highScoreText.text = "High Score: " + highscore.ToString();

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (energy == 0)
        {
            playButton.interactable = false;
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            
            if(energyReadyString == String.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                Debug.Log("Invoking energy recharge in " + (energyReady - DateTime.Now).Seconds + " seconds");
                Invoke(nameof(EnergyRecharge),(energyReady - DateTime.Now).Seconds);
            }
        }
        else
        {
            playButton.interactable = true;
        }

        energyText.text = "Play " + energy;
    }

    private void EnergyRecharge()
    {
        Debug.Log("energy recharge");
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = "Play " + energy;
    }

    public void OnPlayButtonClick()
    {
        if (energy == 0)
            return;
        
        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);
        Debug.Log("energy is " + energy);
        if (energy == 0)
        {
            playButton.interactable = false;
            SetRechargeDate();
        }

        SceneManager.LoadScene("Game");
        //energyText.text = "Play " + energy;
    }

    private void SetRechargeDate()
    {
        DateTime energyReadyTime = DateTime.Now.AddMinutes(energyRechargeDurationMinutes);
        PlayerPrefs.SetString(EnergyReadyKey, energyReadyTime.ToString());
        Debug.Log("set energy recharge time to : " + energyReadyTime.ToString());
        
#if UNITY_ANDROID
        androidNotificationHandler.ScheduleNotification(energyReadyTime);
#endif
        
        
    }
}
