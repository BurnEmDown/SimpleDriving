using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDurationMinutes = 1;
    
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private int energy;

    public void Start()
    {
        int highscore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        highScoreText.text = "High Score: " + highscore.ToString();

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            
            if(energyReadyString == String.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
        }

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
            SetRechargeDate();
        
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
