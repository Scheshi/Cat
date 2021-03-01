using Assets.Scripts.Interfaces;
using Assets.Scripts.Views;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class WeaponRotation
    {
        private WeaponView _weapon;
        private IView _targetView;
        private float _aggroRadius;
        private float _coolDown = 3.0f;
        private float _currentCoolDown = 0.0f;
        
        public WeaponRotation(WeaponView view, float aggroRadius, IView targetView)
        {
            _weapon = view;
            _aggroRadius = aggroRadius;
            _targetView = targetView;
        }

        public void Execute()
        {
            if ((_targetView.Transform.position - _weapon.transform.position).sqrMagnitude <=
                _aggroRadius * _aggroRadius)
            {
                _weapon.transform.up = -(_targetView.Transform.position - _weapon.transform.position).normalized;
                Fire();
            }
        }

        private void Fire()
        {
            if (_currentCoolDown <= Time.time)
            {
                var bullet = Object.Instantiate(_weapon.Weapon.Bullet, _weapon.Weapon.Barrel.transform.position,
                    _weapon.Weapon.Barrel.transform.rotation);
                bullet.AddForce(-_weapon.Weapon.Barrel.transform.up * 15.0f, ForceMode2D.Impulse);
                _currentCoolDown = Time.time + _coolDown;
            }
        }
    }
}