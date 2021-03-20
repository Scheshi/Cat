using Datas;
using UnityEngine;

namespace Models
{
    public class PatrolPathFindingModel
    {
        #region Fields
  
        private readonly AIConfig _config;
        private Transform _target;
        private int _currentPointIndex;
  
        #endregion

  
        #region Class life cycles

        public PatrolPathFindingModel(AIConfig config)
        {
            _config = config;
            _target = GetNextWaypoint();
        }

        #endregion

  
        #region Methods
  
        public Vector2 CalculateVelocity(Vector2 fromPosition)
        {
            var sqrDistance = Vector2.SqrMagnitude((Vector2)_target.position - fromPosition);
            if (sqrDistance <= _config.MinSqrDistanceToTarget)
            {
                _target = GetNextWaypoint();
            }

            var direction = ((Vector2)_target.position - fromPosition).normalized;
            return _config.Speed * direction;
        }
  
        private Transform GetNextWaypoint()
        {
            _currentPointIndex = (_currentPointIndex + 1) % _config.Waypoints.Length;
            return _config.Waypoints[_currentPointIndex];
        }
  
        #endregion

    }
}