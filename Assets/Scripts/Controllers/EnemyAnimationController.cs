using Assets.Scripts.Enums;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class EnemyAnimationController
    {
        private AnimStateMachineController _animController;
        private ObjectView _view;

        public EnemyAnimationController(ObjectView view, AnimStateMachineController animController)
        {
            _animController = animController;
            _view = view;
        }

        public void Update()
        {
            if (_view.Rigidbody.velocity.x > 0.0f || _view.Rigidbody.velocity.x < 0.0f)
            {
                if (_view.Rigidbody.velocity.x < 0.0f && _view.Transform.localScale.x > 0)
                {
                    var localScale = _view.Transform.localScale;
                    _view.Transform.localScale = new Vector3(localScale.x * -1.0f, localScale.y, localScale.z);
                }
                else if(_view.Rigidbody.velocity.x > 0.0f && _view.Transform.localScale.x < 0)
                {
                    var localScale = _view.Transform.localScale;
                    _view.Transform.localScale = new Vector3(localScale.x * -1.0f, localScale.y, localScale.z);
                }

                _animController.StartAnimation(AnimState.Run, true);
            }
            else
            {
                _animController.StartAnimation(AnimState.Idle);
            }
            _animController.Execute();
        }
    }
}