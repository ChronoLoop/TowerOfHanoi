using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }
}