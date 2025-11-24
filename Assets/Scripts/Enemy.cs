using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _fallingAngle = 30f;
    [SerializeField] private float _fallingVelocity = 0.7f;

    private Rigidbody2D _rigidbody2D;
    
    public event Action<Enemy> OnEnemyDied;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Напишите логику уничтожения зомби тут
        var collisionRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.CompareTag(GlobalConstants.SKULL_TAG))
        {
            Die();
        }
        else if (IsEnemyRotate())
        {
            Die();
        }
        else if (IsEnemyHit(collisionRigidbody))
        {
            Die();
        }
    }

    private bool IsEnemyRotate()
    {
        var rotation = _rigidbody2D.rotation;
        return rotation <= -_fallingAngle || rotation >= _fallingAngle;

    }

    private bool IsEnemyHit(Rigidbody2D collisionRigidbody)
    {
        return collisionRigidbody != null && collisionRigidbody.velocity.magnitude >= _fallingVelocity;
    }

    private void Die()
    {
        OnEnemyDied?.Invoke(this);
        // Создаем эффект "взрыв" на месте убитого зомби.
        CreateExplosion();
        // Проигрываем звук смерти зомби.
        PlayDeathSound();
        // Разрушаем объект зомби.
        Destroy(gameObject);
    }

    private void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathSound, transform.position);
    }
    
    private void CreateExplosion()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }
}