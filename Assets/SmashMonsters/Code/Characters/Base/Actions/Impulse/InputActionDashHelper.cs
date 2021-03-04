using System.Collections;
using SmashMonsters.Code.Characters.Base.Jump;
using SmashMonsters.Code.Characters.Base.Movement;
using SmashMonsters.Player.Input;
using UnityEngine;

namespace SmashMonsters.Code.Characters.Base.Actions.Impulse
{
	public class InputActionDashHelper
	{
		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/

		private Rigidbody2D _rigidbody;

		private CharacterInputController _inputController;

		private CharacterMovementController _movementController;

		private CharacterTurnController _turnController;

		private CharacterJumpController _jumpController;

		private CharacterActionController _actionController;

		public CharacterActionController CharacterActionController
		{
			set
			{
				_actionController = value;
				_rigidbody = _actionController.GetComponent<Rigidbody2D>();
				_inputController = _actionController.GetComponent<CharacterInputController>();
				_movementController = _actionController.GetComponent<CharacterMovementController>();
				_turnController = _actionController.GetComponent<CharacterTurnController>();
				_jumpController = _actionController.GetComponent<CharacterJumpController>();
			}
		}

		/*----------------------------------------------------------------------------------------*
	     * Inject
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void Dash(float force, float duration)
		{
			Vector2 direction = new Vector2(_turnController.IsFacingLeft ? -1f : 1f, 0);
			Dash(direction, force, duration);
		}

		public void Dash(Vector2 direction, float force, float duration)
		{
			_actionController.StartCoroutine(DashCoroutine(direction, force, duration));
		}

		public void Dash(float dashForce, float dashDuration, float pauseTime, bool allowSameDirection,
			bool keepLastDirection, int dashesCountMax)
		{
			_actionController.StartCoroutine(MultipleDashesCoroutine(dashForce, dashDuration, pauseTime,
				allowSameDirection, keepLastDirection, dashesCountMax));
		}

		/*----------------------------------------------------------------------------------------*
		 * Coroutines
		 *----------------------------------------------------------------------------------------*/

		private IEnumerator DashCoroutine(Vector2 direction, float force, float waitTime)
		{
			bool canMoveBkp = _movementController.CanMove.Value;
			_movementController.CanMove.Value = false;
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
			_rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
			float gravityScaleBkp = RemoveGravityScale();
			yield return new WaitForSeconds(waitTime);
			RecoverGravityScale(gravityScaleBkp);
			_movementController.CanMove.Value = canMoveBkp;
		}

		private IEnumerator MultipleDashesCoroutine(float dashForce, float dashDuration, float pauseTime,
			bool allowSameDirection, bool keepLastDirection, int dashesCountMax)
		{
			return ControllableObjCoroutine(pauseTime, allowSameDirection, keepLastDirection, dashesCountMax, direction =>
			{
				Dash(direction, dashForce, dashDuration);
			});
		}

		public IEnumerator ControllableObjCoroutine(float pauseTime,
			bool allowSameDirection, bool keepLastDirection, int dashesCountMax, UpdateDirectionCallback onUpdateDirection,
			EndControlCallback onEndControl = null)
		{
			_movementController.CanMove.Value = false;
			_jumpController.CanChangeGravityScale.Value = false;
	    
			float gravityScaleBkp = RemoveGravityScale();
	    
			float horizontalInput = _inputController.Movement.HorizontalRaw;
			float verticalInput = _inputController.Movement.VerticalRaw;
	    
			Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;

			int count = 0;
	    
			while (count < dashesCountMax && direction.magnitude > 0)
			{
				onUpdateDirection?.Invoke(direction);
				yield return new WaitForSeconds(pauseTime);
		    
				horizontalInput = _inputController.Movement.HorizontalRaw;
				verticalInput = _inputController.Movement.VerticalRaw;
		    
				Vector2 directionBkp = direction;
		    
				if (horizontalInput != 0 || verticalInput != 0)
				{
					direction = new Vector2(horizontalInput, verticalInput).normalized;
				}
				else if (!keepLastDirection)
				{
					direction = Vector2.zero;
				}

				if (!allowSameDirection && direction == directionBkp)
				{
					direction = Vector2.zero;
				}

				count++;
			}
	    
			RecoverGravityScale(gravityScaleBkp);
			_jumpController.CanChangeGravityScale.Value = true;
			_movementController.CanMove.Value = true;
			onEndControl?.Invoke();
		}

		private float RemoveGravityScale()
		{
			float gravityScaleBkp = _rigidbody.gravityScale;
			_rigidbody.gravityScale = 0;
			return gravityScaleBkp;
		}

		private void RecoverGravityScale(float gravityScaleBkp)
		{
			_rigidbody.gravityScale = gravityScaleBkp;
			_rigidbody.velocity = Vector2.zero;
		}

		/*----------------------------------------------------------------------------------------*
		 * Inner Classes and Delegates
		 *----------------------------------------------------------------------------------------*/

		public delegate void UpdateDirectionCallback(Vector2 direction);

		public delegate void EndControlCallback();

	}
}
