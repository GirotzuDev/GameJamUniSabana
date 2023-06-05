using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //public AudioSource soundEffectSourc;
    public AudioSource musicSource;

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia del SoundManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}

