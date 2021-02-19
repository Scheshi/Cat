using System.Collections;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class AnimStateMachineController
    {
        private AnimConfig _config;
        private SpriteRenderer _renderer;
        private float _animSpeed;
        private AnimationList _currentAnimation;
        private float _counter = 0;
        private bool _isLoop = false;
        
        public AnimStateMachineController(AnimConfig config, SpriteRenderer renderer, float speed)
        {
            _config = config;
            _renderer = renderer;
            _animSpeed = speed;
        }

        private void AnimationPlaying()
        {
            if (_counter >= _currentAnimation.AnimSprites.Count)
            {
                if(!_isLoop) StartAnimation(AnimState.Idle);
                _counter = 0.0f;
                
            }
            else
            {
                _renderer.sprite = _currentAnimation.AnimSprites[(int)_counter];
                _counter += Time.deltaTime * _animSpeed;
            }
        }

        public void StartAnimation(AnimState state, bool isLoop = false)
        {
            if (_currentAnimation.AnimState != state)
            {
                _currentAnimation = _config.Animations.FirstOrDefault(x => x.AnimState == state);
                _isLoop = isLoop;
            }
            else return;
        }

        public void Execute()
        {
            AnimationPlaying();
        }
    }
}