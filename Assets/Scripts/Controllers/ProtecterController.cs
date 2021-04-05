using System;
using Assets.Scripts.Interfaces;
using Models;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ProtecterController : IProtected
    {
        #region Fields

        private readonly ObjectView _view;
        private readonly ProtecterModel _model;
        //private readonly AIDestinationSetter _destinationSetter;
        private readonly Seeker _seeker;
        //private readonly PatrolAIPath _patrol;

        #endregion
 

        #region Class life cycles
  
        public ProtecterController(ObjectView view, ProtecterModel model, Seeker seeker)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
            //_destinationSetter = destinationSetter != null ? destinationSetter : throw new ArgumentNullException(nameof(patrol));
            //_patrol = patrol != null ? patrol : throw new ArgumentNullException(nameof(model));
            _seeker = seeker != null ? seeker : throw new ArgumentNullException(nameof(seeker));
            FinishProtection();
        }

        #endregion


        #region Methods

        public void StartProtection(Transform target)
        {
            _model.SetTarget(target);
        }

        public void FinishProtection()
        {
            _model.SetTarget(_model.GetNextTarget());
        }
        
        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * Time.fixedDeltaTime;
            _view.Rigidbody.velocity = newVelocity;
        }
  
        public void RecalculatePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_view.Rigidbody.position, _model.Target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;
            _model.UpdatePath(p);
        }

        #endregion

    }
}