using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    private void Start()
    {
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }
}