using UnityEngine;

public class SaveManager
{
    #region Fields
    private SettingsData settings = new SettingsData();
    #endregion

    #region Properties
    public int PlayerColor 
    { 
        get { return settings.playerColor; }
        set { settings.playerColor = value; }
    }
    public float SoundVolume 
    { 
        get { return settings.soundVolume; }
        set { settings.soundVolume = value; }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Save Settings to Object as JSON
    /// </summary>
    public void SaveData()
    {
        string saveJson = JsonUtility.ToJson(settings);
        PlayerPrefs.SetString("Data", saveJson);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// Load Settings from Object as JSON
    /// </summary>
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("Data"))
        {
            string saveJson = PlayerPrefs.GetString("Data");
            settings = JsonUtility.FromJson<SettingsData>(saveJson);
        }
        else { SaveData(); }
    }
    #endregion
}
