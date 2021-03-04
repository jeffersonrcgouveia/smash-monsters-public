using AvoEx.ObservableType;
using SmashMonsters.Player.Input;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Jump
{
	public class CharacterJumpController : MonoBehaviour
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

		private Animator _animator;

		private Rigidbody2D _rigidbody;

		private CharacterInputController _inputController;

		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Callbacks
	     *----------------------------------------------------------------------------------------*/

		public JumpLaterCallback OnCanJump { get; set; }

		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private int jumpHeight;

		[SerializeField]
		private int jumpCount = 2;
	
		[SerializeField]
		private float initialFallMultiplier = 3.7f;
	
		[SerializeField]
		private float fallMultiplier = 0.7f;
	
		[SerializeField]
		private float jumpImpulseGravityScale = 2.0f;

		public ObBool JumpLater { get; } = new ObBool();

		public ObBool CanChangeGravityScale { get; } = new ObBool(true);
	
		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/

		private int _jumpCounter;
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			_animator = GetComponent<Animator>();
			_rigidbody = GetComponentInChildren<Rigidbody2D>();
			_inputController = GetComponent<CharacterInputController>();
		
			_inputController.Movement.IsJumping.AddObserver(isJumping =>
			{
				if (!isJumping) return;
				if (OnCanJump != null && !OnCanJump.Invoke())
				{
					JumpLater.Value = true;
				}
				Jump();
			});
		}

		private void Update()
		{
			HandleJumpGravity();
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Ground"))
			{
				_jumpCounter = jumpCount;
			}
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void HandleJumpGravity()
		{
			if (!CanChangeGravityScale) return;
			if (_rigidbody.velocity.y < -0.1f && _rigidbody.velocity.y > -5)
			{
				_rigidbody.gravityScale = initialFallMultiplier;
			}
			else if (_rigidbody.velocity.y < -0.1f)
			{
				_rigidbody.gravityScale = fallMultiplier;
			}
			else if (_rigidbody.velocity.y > 0.1f)
			{
				_rigidbody.gravityScale = jumpImpulseGravityScale;
			}
			else
			{
				_rigidbody.gravityScale = 1.0f;
			}
		}

		public void JumpIfJumpLaterIsTrue()
		{
			if (!JumpLater) return;
			JumpLater.Value = false;
			Jump();
		}

		private void Jump()
		{
			if (JumpLater.Value || _jumpCounter == 0) return;
			_rigidbody.velocity = Vector2.up * jumpHeight;
			_jumpCounter--;
			_animator.SetInteger("JumpCounter", jumpCount - _jumpCounter);
		}

		public void ResetJumpCounter()
		{
			_jumpCounter = 0;
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

		public delegate bool JumpLaterCallback();

	}
}
