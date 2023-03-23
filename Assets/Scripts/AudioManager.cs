using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    AudioClip jumpAudio;
    [SerializeField]
    AudioClip throwenAudio;
    [SerializeField]
    AudioClip buttonAudio;

    AudioSource audioSource;
    #endregion

    #region Properties
    public AudioSource AudioSource
    { 
        get { return audioSource; }
    }
    public float Volume
    {
        set
        {
            audioSource.volume = value;
        }
    }
    #endregion

    #region Methods
    private void Awake()
    {
       audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Rods.OnThrowPlayer += PlayThrowenAudio; 
        PlayerControl.OnJump += PlayJumpAudio;
        GameManager.OnButton += PlayButtonAudio;
    }
    private void OnDisable()
    {
        Rods.OnThrowPlayer -= PlayThrowenAudio;
        PlayerControl.OnJump -= PlayJumpAudio;
        GameManager.OnButton -= PlayButtonAudio;
    }
    private void PlayJumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    private void PlayThrowenAudio()
    {
        audioSource.clip = throwenAudio;
        audioSource.Play();
    }
    private void PlayButtonAudio()
    {
        audioSource.clip = buttonAudio;
        audioSource.Play();
    }
    #endregion
}
