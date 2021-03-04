using SmashMonsters.Code.Characters.Base.Jump;
using SmashMonsters.Code.Characters.Base.Movement;
using SmashMonsters.Player.Input;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.ImpulseDown
{
	public class CharacterImpulseDownController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Constants
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Injects
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/

		private Rigidbody2D _rigidbody;

		private CharacterMovementController _movementController;

		private CharacterGroundedController _groundedController;

		private CharacterJumpController _jumpController;

		private CharacterInputController _inputController;

		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private int impulseDownForce;

		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/

		private bool _canImpulseDown;

		private bool _isImpulsed;

		private float _impulseLastHorizontalValue;

		private float _impulseTime;
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
			_movementController = GetComponent<CharacterMovementController>();
			_groundedController = GetComponent<CharacterGroundedController>();
			_jumpController = GetComponent<CharacterJumpController>();
			_inputController = GetComponent<CharacterInputController>();
		}

		protected virtual void Update()
		{
			HandleImpulseDown();
			UpdateLerpImpulse();
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Ground"))
			{
				_isImpulsed = false;
			}
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void HandleImpulseDown()
		{
			float verticalInput = _inputController.Movement.Vertical;
			if (verticalInput == 0)
			{
				_canImpulseDown = true;
			}

			if (!_groundedController.IsGrounded && _canImpulseDown && !_isImpulsed && verticalInput == -1)
			{
				_rigidbody.velocity = new Vector2(0, 0);
				_rigidbody.AddForce(Vector2.down * impulseDownForce, ForceMode2D.Impulse);
				_isImpulsed = true;
				_jumpController.ResetJumpCounter();
			}

			if (verticalInput == -1)
			{
				_canImpulseDown = false;
			}
		}

		public void StartLerpImpulse(float impulseTime)
		{
			_impulseTime = impulseTime;
			_impulseLastHorizontalValue = Mathf.Clamp(_inputController.Movement.HorizontalRaw.Value, -1, 1);
		}

		private void UpdateLerpImpulse()
		{
			if (_impulseLastHorizontalValue < -0.1f || _impulseLastHorizontalValue > 0.1f)
			{
				_impulseLastHorizontalValue = Mathf.Lerp(_impulseLastHorizontalValue, 0, _impulseTime * Time.deltaTime);
				transform.position += new Vector3(_movementController.Speed * _impulseLastHorizontalValue * Time.deltaTime, 0, 0);
			}
		}

		public void StopLerpImpulse()
		{
			_impulseLastHorizontalValue = 0;
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Utility Methods
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Coroutines
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/
	
	}
}
