using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour, IDamagable
{
    [SerializeField] private int _startHealth;
    [SerializeField] private float _speed;
    [SerializeField] private float _invincibilityTime;


    public float Health { get; private set; }


    private Vector2 _direction;
    private bool _invincible;


    private void Update()
    {
        transform.position += new Vector3(_direction.x, 0, _direction.y).normalized * _speed * Time.deltaTime;
    }


    public void Move(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }
    private IEnumerator coroutine;

    public void TakeDamage(float damage)
    {
        if (_invincible) { return; }

        Health -= damage;

        if (Health <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(StartInvincibility());
    }
    

    private void Die()
    {
        
    }
    private IEnumerator StartInvincibility()
    {
        float time = _invincibilityTime;

        _invincible = true;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        _invincible = false;
    }
}
