using UnityEngine;
using Main;
namespace CameraNamespace
{
    public class CameraManagerScript : MonoBehaviour
    {
        [SerializeField] private Transform _character;
        [SerializeField] private Transform _trajectoryFollower;
        [SerializeField] private float _smoothSpeed = 0.1f;
        [SerializeField] private GameManagerScript _gameController;
        private Vector3 _offset;
        private void Update()
        {
            if (_gameController.CharacterIsThrown)
            {
                Vector3 desiredPosition = _character.position + _offset;
                transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            }
            else
            {
                Vector3 desiredPosition = new Vector3(_character.position.x * 3f, _character.position.y + 1.5f, _character.position.z / 1.3f - 3.4f);
                transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
                var lastCameraPosition = transform.position;
                var lastTargetPosition = _character.position;
                _offset = lastCameraPosition - lastTargetPosition;
                transform.LookAt(_trajectoryFollower);
            }
        }
    }
}