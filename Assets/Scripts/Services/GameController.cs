using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class GameController: MonoBehaviour
    {
        [SerializeField] private AnimConfig _config;
        [SerializeField] private ObjectView _player;

        private AnimStateMachineController _animatorController;

        private void Start()
        {
            _animatorController = new AnimStateMachineController(_config, _player.SpriteRenderer, 12.0f);
            _animatorController.StartAnimation(AnimState.Run);
        }

        private void Update()
        {
            //Сделал себе для тестов, потом уберу
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _animatorController.StartAnimation(AnimState.Attack, true);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                _animatorController.StartAnimation(AnimState.Jump, true);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _animatorController.StartAnimation(AnimState.Run, true);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                _animatorController.StartAnimation(AnimState.Idle, true);
            }
            _animatorController.Execute();
        }
    }
}