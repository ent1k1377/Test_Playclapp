using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Сube
{
    public class CubeMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _distance;
        [SerializeField] private float _speed;

        private Vector3 _initialPosition;

        private void Start() => 
            _initialPosition = transform.position;

        private void Update()
        {
            if (Vector3.Distance(_initialPosition, transform.position) <= _distance)
                transform.Translate(_direction * _speed * Time.deltaTime);
            else
                gameObject.Deactivate();
        }

        public void SetDistance(float distance) =>
            _distance = distance;

        public void SetSpeed(float speed) =>
            _speed = speed;
    }
}
