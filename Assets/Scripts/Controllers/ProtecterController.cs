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
        private readonly AIDestinationSetter _destinationSetter;
        private readonly PatrolAIPath _patrol;
  
        private bool _isPatrolling;

        #endregion
 

        #region Class life cycles
  
        public ProtecterController(ObjectView view, ProtecterModel model, AIDestinationSetter destinationSetter, PatrolAIPath patrol)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
            _destinationSetter = destinationSetter != null ? destinationSetter : throw new ArgumentNullException(nameof(patrol));
            _patrol = patrol != null ? patrol : throw new ArgumentNullException(nameof(model));
        }

        public void Init()
        {
            _destinationSetter.target = _model.GetNextTarget();
            _isPatrolling = true;
            _patrol.TargetReached += OnTargetReached;
        }

        public void Deinit()
        {
            _patrol.TargetReached -= OnTargetReached;
        }

        #endregion


        #region Methods

        private void OnTargetReached(object sender, EventArgs e)
        {
            _destinationSetter.target = _isPatrolling
                ? _model.GetNextTarget()
                : _model.GetClosestTarget(_view.Transform.position);
        }
  
        public void StartProtection(GameObject invader)
        {
            _isPatrolling = false;
            _destinationSetter.target = invader.transform;
        }

        public void FinishProtection(GameObject invader)
        {
            _isPatrolling = true;
            _destinationSetter.target = _model.GetClosestTarget(_view.Transform.position);
        }

        #endregion

    }
}