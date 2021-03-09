using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class PlayerMove
    {
        private IView _view;
        private AnimStateMachineController _animController;
        private GameController _controller;
        private float _speed = 3.0f;
        private float _jumpForce = 7.0f;
        private bool _isGrounded = true;
        private float _currentAxis = 1.0f;
        private float _healthCount = 3;

        public PlayerMove(IView view, AnimStateMachineController controller, GameController gameController)
        {
            _view = view;
            _animController = controller;
            _controller = gameController;
        }
        
        public void Execute()
        {
            CheckRotate();
            Movement();
            CheckJump();
            Jump();
        }

        public void FixedExecute()
        {
        }

        private void CheckRotate()
        {
            var transform = _view.Transform;
            var axis = Input.GetAxis("Horizontal");
            if (axis > 0 && _currentAxis < 0 || axis < 0 && _currentAxis > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y,
                    transform.localScale.z);
                _currentAxis = transform.localScale.x;
            }
        }

        public void Damage(float point)
        {
            _healthCount -= point;
            if(_healthCount <= 0) _controller.Restart();
        }
        
        private void Movement()
        {
            var axis = Input.GetAxis("Horizontal");
            if (axis > 0 || axis < 0)
            {
                _view.Rigidbody.velocity =
                    new Vector2(Input.GetAxis("Horizontal")  * _speed, _view.Rigidbody.velocity.y);
                _animController.StartAnimation(AnimState.Run);
            }
            else _animController.StartAnimation(AnimState.Idle);

        }

        private void CheckJump()
        {
            if (_view.Rigidbody.velocity.y >= -0.01f && _view.Rigidbody.velocity.y <= 0.01f)
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
                _animController.StartAnimation(AnimState.Jump);
            }
        }

        private void Jump()
        {
            if (_isGrounded && Input.GetButtonDown("Vertical"))
            {
                _view.Rigidbody.AddForce(new Vector2(0.0f, _jumpForce * _view.Rigidbody.mass), ForceMode2D.Impulse);
            }
        }
    }
}