using System;
using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Views;
using Datas;
using Pathfinding;
using UnityEngine;


namespace Assets.Scripts.Services
{
    [Serializable]
    public struct AnimatedObject
    {
        public ObjectView Obj;
        public AnimConfig AnimationConfig;
        public AIConfig Config;
    }

    public class GameController: MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private AnimatedObject _player;
        [SerializeField] private AnimatedObject[] _enemies;
        [SerializeField] private Seeker _seeker;
        
        [SerializeField] private WeaponView _weapon;
        [SerializeField] private GameObject _backGroundObject;
        [SerializeField] private ObjectView[] _coins;
        [SerializeField] private ObjectView[] _deathZones;
        [SerializeField] private ObjectView _endPoint;

        private AnimStateMachineController _animatorController;
        private PlayerMove _playerMoveController;
        private WeaponRotation _weaponRotation;
        private CameraMove _camera;
        private BackGroundController _backGround;
        private CollisionManager _collisionManager;
        private List<StalkerAIController> _enemiesControllers = new List<StalkerAIController>();

        private void Awake()
        {
            _animatorController = new AnimStateMachineController(_player.AnimationConfig, _player.Obj.SpriteRenderer, 12.0f);
            _animatorController.StartAnimation(AnimState.Idle);
            _playerMoveController = new PlayerMove(_player.Obj, _animatorController, this);
            _weaponRotation = new WeaponRotation(_weapon, 5.0f, _player.Obj);
            _camera = new CameraMove(Camera.main, _player.Obj.transform);
            _backGround = new BackGroundController(_backGroundObject.transform, Camera.main);
            _collisionManager = new CollisionManager(_playerMoveController, _player.Obj, _coins, _deathZones, _endPoint, this);
            foreach (var enemy in _enemies)
            {
                _enemiesControllers.Add(new StalkerAIController(enemy.Obj, new StalkerPathFindingModel(enemy.Config), _seeker, _player.Obj.Transform));
            }
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
            
            foreach (var enemy in _enemiesControllers)
            {
                enemy.FixedUpdate();
            }
        }

        public void Restart()
        {
            _collisionManager.Refresh();
            _player.Obj.Transform.position = _start.position;
            _playerMoveController.Damage(-3.0f);
        }
    }
}