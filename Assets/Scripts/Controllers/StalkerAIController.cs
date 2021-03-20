﻿using System;
using Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class StalkerAIController
    {
        #region Fields
        private readonly ObjectView _view;
        private readonly StalkerPathFindingModel _model;
        private readonly Seeker _seeker;
        private readonly Transform _target;
        #endregion
  
        #region Class life cycles
  
        public StalkerAIController(ObjectView view, StalkerPathFindingModel model, Seeker seeker, Transform target)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
            _seeker = seeker != null ? seeker : throw new ArgumentNullException(nameof(seeker));
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));
        }
  
        #endregion

        #region Methods

        public void FixedUpdate()
        {
            var newVelocity = _model.CalculateVelocity(_view.Transform.position) * Time.fixedDeltaTime;
            _view.Rigidbody.velocity = newVelocity;
        }
  
        public void RecalculatePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_view.Rigidbody.position, _target.position, OnPathComplete);
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