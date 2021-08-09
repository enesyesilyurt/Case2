using UnityEngine;
using Main;
namespace Character
{
    public class CharacterManagerScript : MonoBehaviour
    {
        [SerializeField] private GameManagerScript _gameController;
        [SerializeField] private Transform _restartButton;
        private Rigidbody _rigidbody;
        private void Start()
        {
            _rigidbody = transform.GetComponent<Rigidbody>();
        }
        public void CharacterAddForce(float characterForce)
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = transform.forward * _gameController.SeatSlingDistance *characterForce;
            transform.GetComponent<Animator>().SetTrigger("StartFalling");
        }
        private void OnCollisionEnter(Collision collision) //this func runs when the character lands on the ground.
        {
            if (collision.gameObject.layer == 7)
            {
                _rigidbody.isKinematic = true;
                transform.GetComponent<Animator>().SetTrigger("FallingEnd");
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                _restartButton.gameObject.SetActive(true);
            }
        }
    }
}
