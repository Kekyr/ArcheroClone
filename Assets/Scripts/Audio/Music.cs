using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _music;

    private AudioSource _musicSource;
    private IEnumerator _updateMusicWithFade;
    private float _defaultVolume;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _musicSource = GetComponent<AudioSource>();
        _defaultVolume = _musicSource.volume;
    }

    private void Update()
    {
        if (_musicSource.time == 0)
        {
            PlayMusic();
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        int lastIndex = _music.Count - 1;
        int randomIndex = Random.Range(0, lastIndex);

        _musicSource.clip = _music[randomIndex];
        _musicSource.Play();
    }
}