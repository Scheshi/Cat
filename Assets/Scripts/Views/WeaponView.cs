using System;
using UnityEngine;


namespace Assets.Scripts.Views
{
    public class WeaponView : MonoBehaviour
    {
        private BarrelView _weapon;

        public BarrelView Weapon => _weapon;

        private void Awake()
        {
            _weapon = GetComponentInChildren<BarrelView>();
        }
    }
}