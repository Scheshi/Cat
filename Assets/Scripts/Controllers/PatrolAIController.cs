using System;
using Models;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PatrolAIController
    {
        #region Fields
  
        private readonly ObjectView _view;
        private readonly PatrolPathFindingModel _model;

        #endregion
  
  
        #region Class life cycles
  
        public PatrolAIController(ObjectView view, PatrolPathFindingModel model)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        }

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * Time.fixedDeltaTime;
            _view.Rigidbody.velocity = newVelocity;
        }
  
        #endregion

    }
}