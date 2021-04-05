using Datas;
using Pathfinding;
using UnityEngine;

namespace Models
{
    public class ProtecterModel
    {
        #region Fields

        private AIConfig _config;
        private readonly Transform[] _waypoints;
        private int _currentPointIndex;
        private Transform _target;
        private Path _path;

        #endregion

        #region Properties

        public Transform Target => _target;
        
        #endregion

        #region Methods

        public ProtecterModel(AIConfig config)
        {
            _waypoints = config.Waypoints;
            _config = config;
            _currentPointIndex = 0;
        }
  
        public Transform GetNextTarget()
        {
            if (_waypoints == null) return null;
            _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
            return _waypoints[_currentPointIndex];
        }

        /*public Transform GetClosestTarget(Vector2 fromPosition)
        {
            if (_waypoints == null) return null;
    
            var closestIndex = 0;
            var closestDistance = 0.0f;
            for (var i = 0; i < _waypoints.Length; i++)
            {
                var distance = Vector2.Distance(fromPosition, _waypoints[i].position);
                if (closestDistance > distance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            _currentPointIndex = closestIndex;
            return _waypoints[_currentPointIndex];
        }*/
        
        public Vector2 CalculateVelocity(Vector2 fromPosition)
        {
            if (_path == null) return Vector2.zero;
                if (_currentPointIndex >= _path.vectorPath.Count) return Vector2.zero;

                var direction = ((Vector2)_path.vectorPath[_currentPointIndex] - fromPosition).normalized;
                var result = _config.Speed * direction;
                var sqrDistance = Vector2.SqrMagnitude((Vector2) _path.vectorPath[_currentPointIndex] - fromPosition);
                if (sqrDistance <= _config.MinSqrDistanceToTarget)
                {
                    _currentPointIndex++;
                }
                return result;
        }
        
        public void UpdatePath(Path p)
        {
            _path = p;
            _currentPointIndex = 0;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        #endregion

    }
}