using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Data Object")]
    [SerializeField] private Weapon _weapon;

    
    private string _name;
    private int _damage;
    private string _description;
    private float _range;
    private TypeWeapon _typeWeapon;


    [SerializeField] private float _attackCooldown = 1.0f;
    [SerializeField] private Vector3 _direction;

    private int _maxDamage;
    private int _minDamage;
    private bool _canAttack = true;

    [SerializeField] private Collider _collider;
    private IAttacked _attacked;


    private StarterAssets.StarterAssetsInputs _input;


    private void OnEnable()
    {

    }


    private void OnDisable()
    {

    }


    private void Start()
    {
        Initialize();
        _input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
        _collider = GetComponent<Collider>();
        
    }


    private void Update()
    {
        if (_input.attack)
        {
            if (_canAttack)
            {
                _canAttack = false;
                StartCoroutine(ResetAttackCooldown());
            }
        }
    }


    public void Initialize()
    {
        _name = _weapon.Name;
        _damage = _weapon.Damage;
        _description = _weapon.Description;
        _range = _weapon.Range;
    }


    public float DamageWeapon => _damage;
    public string NameWeapon => _name;
    public TypeWeapon TypeWeapon => _typeWeapon;


    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy attacked))
        {
            attacked.ApplyDamage(_damage);
        }
    }
}
