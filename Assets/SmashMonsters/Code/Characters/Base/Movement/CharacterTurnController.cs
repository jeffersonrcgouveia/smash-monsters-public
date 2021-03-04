using AvoEx.ObservableType;
using SmashMonsters.Code.Characters.Base.Jump;
using SmashMonsters.Player.Input;
using TopDownMedieval.Plugins.Commons.Utils;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Movement
{
	public class CharacterTurnController : MonoBehaviour
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

		private CharacterGroundedController _groundedController;

		private CharacterJumpController _jumpController;

		private CharacterInputController _inputController;

		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Exposed Callbacks
	     *----------------------------------------------------------------------------------------*/

		public MoveCallback OnMove { get; set; }

		public CanMoveCallback OnCanMove { get; set; }

		public SetCanMoveCallback OnSetCanMove { get; set; }
	
		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/

		public ObBool CanTurn { get; } = new ObBool(true);

		private ObBool IsTurning { get; } = new ObBool();

		public ObBool IsFacingLeft { get; } = new ObBool(true);
	
		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/

		private float _turnLastHorizontalValue;
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			_animator = GetComponent<Animator>();
			_groundedController = GetComponent<CharacterGroundedController>();
			_jumpController = GetComponent<CharacterJumpController>();
			_inputController = GetComponent<CharacterInputController>();
		
			IsTurning.AddObserver(CanTurn =>
			{
				_animator.SetBool("CanTurn", CanTurn);
				if (CanTurn)
				{
					_turnLastHorizontalValue = _inputController.Movement.LastHorizontal;
				}
				OnSetCanMove.Invoke(!CanTurn);
				if (!CanTurn)
				{
					Flip(!IsFacingLeft.Value);
				}
			});

			_jumpController.OnCanJump = () => !IsTurning;
		}

		protected virtual void Update()
		{
			DecelerateTurnRunning();
		}

		private void LateUpdate()
		{
			HandleMovingDirection();
		}

		public void OnTurnAnimEnd()
		{
			IsTurning.Value = false;
			_jumpController.JumpIfJumpLaterIsTrue();
		}

		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/
	
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void DecelerateTurnRunning()
		{
			if (_turnLastHorizontalValue.IsEqualsZero()) return;
			OnMove.Invoke(_turnLastHorizontalValue = Mathf.Lerp(_turnLastHorizontalValue, 0, 10 * Time.deltaTime));
		}

		public virtual void HandleCanTurn()
		{
			if (!CanTurn || !_groundedController.IsGrounded || _inputController.Movement.IsWalking) return;
			float difference = Mathf.Abs(_inputController.Movement.Horizontal - _inputController.Movement.LastHorizontal);
			if (difference > 0.8f)
			{
				IsTurning.Value = true;
			}
		}

		private void HandleMovingDirection()
		{
			if (!OnCanMove.Invoke() || !_groundedController.IsGrounded) return;
			if (_inputController.Movement.Horizontal < 0f)
			{
				Flip(IsFacingLeft.Value = true);
			}
			else if (_inputController.Movement.Horizontal > 0f)
			{
				Flip(IsFacingLeft.Value = false);
			}
		}

		protected virtual void Flip(bool isMovingLeft)
		{
			transform.rotation = Quaternion.Euler(0, isMovingLeft ? 180 : 0, 0);
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

		public delegate void MoveCallback(float value);

		public delegate bool CanMoveCallback();

		public delegate void SetCanMoveCallback(bool canMove);
	
	}
}
