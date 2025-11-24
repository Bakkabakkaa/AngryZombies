using System;
using UnityEngine;

public class Skull : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    
    public event Action OnOutOfMap;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.OUT_OF_MAP_TAG))
        {
            OnOutOfMap?.Invoke();
        }

        if (other.gameObject.CompareTag(GlobalConstants.STONE_TAG))
        {
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
        }
    }
}