using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class CameraMove
    {
        private Camera _camera;
        private Transform _targetTransform;

        public CameraMove(Camera camera, Transform targetTrasform)
        {
            _camera = camera;
            _targetTransform = targetTrasform;
        }

        public void Execute()
        {
            _camera.transform.position = new Vector3(_targetTransform.position.x, _targetTransform.position.y, _camera.transform.position.z);
        }
    }
}