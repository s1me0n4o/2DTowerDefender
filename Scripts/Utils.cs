using UnityEngine;

namespace Assets.ProjectFiles.Scripts
{
    public static class Utils
    {
        private static Camera _mainCamera;

        public static Vector3 GetMouseWorldPosition()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }

            var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            return mouseWorldPosition;
        }

        public static Vector3 GetRandomDirection()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized;
        }

        public static float GetAngleFromVector(Vector3 vector)
        {
            var radians = Mathf.Atan2(vector.y, vector.x);
            var degrees = radians * Mathf.Rad2Deg;
            return degrees;
        }
    }
}
