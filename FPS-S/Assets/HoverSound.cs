using UnityEngine;
using UnityEngine.EventSystems;


public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource source;
    private AudioClip clip;

    private void Awake()
    {
        source = transform.root.GetComponent<AudioSource>();
        clip = source.clip;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(clip);
    }
}