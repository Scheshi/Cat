using System;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class GameController: MonoBehaviour
    {
        [SerializeField] private AnimConfig _config;
        [SerializeField] private ObjectView _player;
        [SerializeField] private WeaponView _weapon;
        [SerializeField] private GameObject _backGroundObject;

        private AnimStateMachineController _animatorController;
        private PlayerMove _playerMoveController;
        private WeaponRotation _weaponRotation;
        private CameraMove _camera;
        private BackGroundController _backGround;

        private void Start()
        {
            _animatorController = new AnimStateMachineController(_config, _player.SpriteRenderer, 12.0f);
            _animatorController.StartAnimation(AnimState.Idle);
            _playerMoveController = new PlayerMove(_player, _animatorController);
            _weaponRotation = new WeaponRotation(_weapon, 5.0f, _player);
            _camera = new CameraMove(Camera.main, _player.transform);
            _backGround = new BackGroundController(_backGroundObject.transform, Camera.main);
        }
        
        private void Update()
        {
            _animatorController.Execute();
            _playerMoveController.Execute();
            _weaponRotation.Execute();
            _camera.Execute();
            _backGround.Execute();
        }

        private void FixedUpdate()
        {
            _playerMoveController.FixedExecute();
        }
    }
}