using UnityEngine;

public class MusicTrackManager : MonoBehaviour
{
    [SerializeField] private AudioClip regularGameplayTrack;
    [SerializeField] private AudioClip bossTrack;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float timeToPlayBossMusic;
    [SerializeField] private float transitionTime = 3f;

    private float timer;
    private bool playingBossTrack;

    private void Start()
    {
        audioSource.clip = regularGameplayTrack;
        audioSource.Play();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (!playingBossTrack && timer >= timeToPlayBossMusic)
        {
            playingBossTrack = true;
            StartCoroutine(TransitionToBossTrack());
        }
    }

    private System.Collections.IEnumerator TransitionToBossTrack()
    {
        var startTime = Time.time;
        var startVolume = audioSource.volume;

        while (Time.time - startTime <= transitionTime)
        {
            var t = (Time.time - startTime) / transitionTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t);

            yield return null;
        }
        
        audioSource.clip = bossTrack;
        audioSource.Play();

        startTime = Time.time;

        while (Time.time - startTime <= transitionTime)
        {
            var t = (Time.time - startTime) / transitionTime;
            audioSource.volume = Mathf.Lerp(0f, startVolume, t);

            yield return null;
        }

        audioSource.volume = startVolume;
    }
}