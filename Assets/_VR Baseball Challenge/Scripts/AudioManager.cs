using Oculus.Interaction;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _lose;
    [SerializeField] private AudioSource _nextLevel;
    [SerializeField] private AudioSource _overTurn;
    [SerializeField] private AudioSource _count;
    [SerializeField] private AudioSource _click;

    public AudioSource BackgroundMusic => _backgroundMusic;
    public AudioSource Win => _win;
    public AudioSource Lose => _lose;
    public AudioSource NextLevel => _nextLevel;
    public AudioSource OverTurn => _overTurn;
    public AudioSource Count => _count;
    public AudioSource Click => _click;
}
