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
    }

    public class GameController: MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private AnimatedObject _player;
        [SerializeField] private AnimatedObject[] _animatedEnemies;

        [SerializeField] private WeaponView _weapon;
        [SerializeField] private GameObject _backGroundObject;
        [SerializeField] private ObjectView[] _coins;
        [SerializeField] private ObjectView[] _deathZones;
        [SerializeField] private ObjectView _endPoint;

        [SerializeField] private GenerateLevelView _generateLevel;

        private AnimStateMachineController _animatorController;
        private PlayerMove _playerMoveController;
        private WeaponRotation _weaponRotation;
        private CameraMove _camera;
        private BackGroundController _backGround;
        private CollisionManager _collisionManager;
        private List<EnemyAnimationController> _enemiesControllers = new List<EnemyAnimationController>();

        private void Awake()
        {
            var generate = new GenerateLevelController(_generateLevel);
            generate.GenerateLevel();
            _animatorController = new AnimStateMachineController(_player.AnimationConfig, _player.Obj.SpriteRenderer, 12.0f);
            _animatorController.StartAnimation(AnimState.Idle);
            _playerMoveController = new PlayerMove(_player.Obj, _animatorController, this);
            _weaponRotation = new WeaponRotation(_weapon, 5.0f, _player.Obj);
            _camera = new CameraMove(Camera.main, _player.Obj.transform);
            _backGround = new BackGroundController(_backGroundObject.transform, Camera.main);
            _collisionManager = new CollisionManager(_playerMoveController, _player.Obj, _deathZones, _endPoint, this);
            foreach (var enemy in _animatedEnemies)
            {
                var anim = new AnimStateMachineController(enemy.AnimationConfig, enemy.Obj.GetComponent<SpriteRenderer>(), 12.0f);
                _enemiesControllers.Add(new EnemyAnimationController(enemy.Obj, anim));
                anim.StartAnimation(AnimState.Run);
            }
            _player.Obj.GetComponent<StoryQuests>().StartStory();
        }
        
        private void Update()
        {
            _animatorController.Execute();
            _playerMoveController.Execute();
            _weaponRotation.Execute();
            _camera.Execute();
            _backGround.Execute();
            foreach (var controller in _enemiesControllers)
            {
                controller.Update();
            }
        }

        private void FixedUpdate()
        {
            _playerMoveController.FixedExecute();
        }

        public void Restart()
        {
            _collisionManager.Refresh();
            _player.Obj.Transform.position = _start.position;
            _playerMoveController.Damage(-3.0f);
        }
    }
}