using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IAttacked
{
    [SerializeField] private string _name;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _velocity;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _acceleration;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _waitSeconds;

    private bool _isApplyDamage = false;

    public EnemyData _enemyProfile;

    
    public delegate void DeathEnemy(EnemyData enemyData);
    public DeathEnemy _deathEnemy;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _health = _maxHealth;
        _velocity = _maxVelocity;

    }

    private void Update()
    {
        if (_animator == null)
            return;
    }


    public void ApplyDamage(int damage)
    {
        _health -= damage;
        _velocity = 0;
        

        if (_animator)
        {
            _animator.SetBool("Hit", false);
            _animator.SetBool("Death", false);
        }

        if (_health <= _maxHealth)
        {
            _isApplyDamage = true;

            if (_isApplyDamage)
            {
                _animator.Play("GetHit");
                _isApplyDamage = false;
            }    
            if(_isApplyDamage == false)
            {
                _velocity = _maxVelocity;
              
            }
        }


        if (_health <= 0)
        {
            Deth();
        }


        if (!this.gameObject.activeSelf)
        {
            return;
        }
    }


    private void Deth()
    {
        _animator.Play("Death");
        GameManager.Instance.OnEnemyDeath?.Invoke(_enemyProfile);
        StartCoroutine(WaitSeconds());
    }


    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(_waitSeconds);
        this.gameObject.SetActive(false);
    }
}
