using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _defaultVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _defaultVolume = _audioSource.volume;
    }

    public void Play(SFXSO sfx)
    {
        int lastIndex = sfx.Clips.Count - 1;
        int randomIndex = Random.Range(0, lastIndex);

        if (sfx.Volume == 0)
            _audioSource.volume = _defaultVolume;
        else
            _audioSource.volume = sfx.Volume;

        _audioSource.PlayOneShot(sfx.Clips[randomIndex]);
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}