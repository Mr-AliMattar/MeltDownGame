using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    AudioManager audioManager;
    [SerializeField]
    Slider soundSlider;
    [SerializeField]
    Color[] colors;

    SaveManager saveManager;
    #endregion

    #region Properties
    public Slider SoundSlider { get; set; }
    public Color[] Colors
    {
        get { return colors; }
    }
    public SaveManager SaveManager 
    { 
        get { return saveManager; }
    }
    #endregion

    #region Events
    public static event Action OnButton;
    #endregion

    #region Unity Mthod
    private void Awake()
    {
        saveManager= new SaveManager();
        saveManager.LoadData();
    }
    private void Start()
    {
        audioManager.Volume = saveManager.SoundVolume;
    }
    #endregion

    #region public Methods
    /// <summary>
    /// Load a scene using sceneNum
    /// </summary>
    /// <param name="sceneNum"></param>
    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    /// <summary>
    /// Quit the App
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
    /// <summary>
    /// To simply active and deactive UI-s
    /// </summary>
    public void SwitchGameObjects(GameObject UI)
    {
        UI.SetActive(!UI.activeSelf);
    }
    public void Observer()
    {
        OnButton?.Invoke();
    }
    //Methods call on settings
    #region Settings Methods
    /// <summary>
    /// Change Colors value to get called by GameManager
    /// </summary>
    /// <param name="myColorNum"></param>
    virtual public void ChangeColor(int myColorNum)
    {
        saveManager.PlayerColor = myColorNum;
        saveManager.SaveData();
    }
    /// <summary>
    /// Change SoundVolume value to get called by GameManager
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSound(float value)
    {
        saveManager.SoundVolume = value;
        saveManager.SaveData();
        audioManager.Volume = value;
    }
    #endregion
    #endregion
}
