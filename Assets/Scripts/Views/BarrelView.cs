using Assets.Scripts.Interfaces;
using UnityEngine;


namespace Assets.Scripts.Views
{
    public class BarrelView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _bulletRigidbody2D;
        private Transform _barrel;


        public Transform Barrel => _barrel;
        public Rigidbody2D Bullet => _bulletRigidbody2D;


        private void Start()
        {
            _barrel = GetComponentInChildren<BarrelMarker>().transform;
        }
    }
}