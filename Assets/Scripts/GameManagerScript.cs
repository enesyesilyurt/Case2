using UnityEngine;
using InputTouchField;
using Trajectory;
using Character;
namespace Main
{
    public class GameManagerScript : MonoBehaviour
    {
        [SerializeField] private float characterForce = 10f;
        [SerializeField] private GameObject _seat;
        [SerializeField] private GameObject _sling;
        [SerializeField] private Transform _character;
        [SerializeField] private Transform _slingMiddlePosition;
        [SerializeField] private TrajectoryRendererScript _trajectoryRenderer;

        private float _seatSlingDistance;
        private bool _touchedTheSeat = false;
        private bool _characterIsThrown = false;
        private CharacterManagerScript _characterController;
        private TouchFieldScript touchField;
        public bool CharacterIsThrown => _characterIsThrown;
        public float SeatSlingDistance => _seatSlingDistance;

        private void Start()
        {
            touchField = FindObjectOfType<TouchFieldScript>();
            _characterController = _character.GetComponent<CharacterManagerScript>();
        }
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                TouchCotrol(touch);
                if (_touchedTheSeat)
                {
                    SeatMovement();
                    SlingBending();

                    _seatSlingDistance = Mathf.Clamp(Vector3.Distance(_seat.transform.position, _slingMiddlePosition.position), 0.3f, 1.8f);
                    _trajectoryRenderer.ShowTrajectory(_character.position, _character.transform.forward * _seatSlingDistance * characterForce);

                    if (touch.phase == TouchPhase.Ended)
                    {
                        CharacterThrown();
                    }
                }
            }
        }
        private void TouchCotrol(Touch touch)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out raycastHit))
            {
                if (raycastHit.collider.tag == "Seat")
                {
                    _touchedTheSeat = true;
                }
            }
        }
        private void SeatMovement()
        {
            var lerpSeatPosition = _seat.transform.position;
            lerpSeatPosition += new Vector3(Time.fixedDeltaTime * touchField.TouchDist.x / 1.8f, 0, Time.fixedDeltaTime * touchField.TouchDist.y / 1.8f);

            lerpSeatPosition = new Vector3(Mathf.Clamp(lerpSeatPosition.x, -1.4f, 1.4f),
                                           Mathf.Clamp(0.9f - _seatSlingDistance / 1.60f, -0.2f, 0.76f),
                                           Mathf.Clamp(lerpSeatPosition.z, -1.8f, -0.25f));

            _seat.transform.position = Vector3.Lerp(_seat.transform.position, lerpSeatPosition, Time.fixedDeltaTime*5);
            _seat.transform.LookAt(_slingMiddlePosition);
        }
        private void CharacterThrown()
        {
            _seat.GetComponent<Rigidbody>().isKinematic = false;
            _trajectoryRenderer.ShowTrajectory(_character.position, _character.transform.forward * _seatSlingDistance * characterForce);
            _characterController.GetComponent<CharacterManagerScript>().CharacterAddForce(characterForce);
            _seat.GetComponent<BoxCollider>().enabled = false;
            _touchedTheSeat = false;
            _characterIsThrown = true;
        }
        private void SlingBending()
        {
            _sling.transform.rotation = new Quaternion(-Vector3.Distance(_seat.transform.position, _slingMiddlePosition.position) / 9,
                        _seat.transform.rotation.y, _sling.transform.rotation.z, _sling.transform.rotation.w);
        }
    }
}