using UnityEngine;


namespace Assets.Scripts.Controllers
{
    public class BackGroundController
    {
        private Transform _backgroundTransform;
        private Camera _camera;

        public BackGroundController(Transform backgroundTransform, Camera camera)
        {
            _backgroundTransform = backgroundTransform;
            _camera = camera;
        }

        public void Execute()
        {
            _backgroundTransform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y,
                _backgroundTransform.transform.position.z);
        }
    }
}