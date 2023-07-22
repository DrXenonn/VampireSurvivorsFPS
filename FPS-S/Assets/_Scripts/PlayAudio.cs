using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
    
}
