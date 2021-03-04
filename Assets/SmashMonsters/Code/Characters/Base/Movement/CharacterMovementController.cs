using AvoEx.ObservableType;
using SmashMonsters.Player.Input;
using UnityEngine;
using UnityEngine.Events;

namespace SmashMonsters.Code.Characters.Base.Movement
{
	public class CharacterMovementController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * Constants
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
		 * Inject
		 *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Components
	     *----------------------------------------------------------------------------------------*/

		private Animator _animator;

		private CharacterTurnController _turnController;

		private CharacterInputController _inputController;

		/*----------------------------------------------------------------------------------------*
	     * Exposed Events
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private UnityEvent onInputHorizontal;

		/*----------------------------------------------------------------------------------------*
	     * Exposed Callbacks
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Exposed Variables
	     *----------------------------------------------------------------------------------------*/

		[field:SerializeField]
		public int Speed { get; private set; }

		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/

		public ObBool CanMove { get; } = new ObBool(true);

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Start()
		{
			_animator = GetComponent<Animator>();
			_turnController = GetComponent<CharacterTurnController>();
			_inputController = GetComponent<CharacterInputController>();
		
			_inputController.Movement.Horizontal.AddObserver(horizontal =>
			{
				float horizontalValue = ClampWalkingInput(horizontal);
				_animator.SetFloat("Horizontal", horizontalValue);
				onInputHorizontal?.Invoke();
			});

			_turnController.OnMove += Move;
			_turnController.OnCanMove += () => CanMove;
			_turnController.OnSetCanMove += value => CanMove.Value = value;
		}

		protected virtual void Update()
		{
			HandleMovement();
		}

		/*----------------------------------------------------------------------------------------*
	     * Animation Events
	     *----------------------------------------------------------------------------------------*/

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void HandleMovement()
		{
			if (!CanMove) return;
			Move(_inputController.Movement.Horizontal);
		}

		private void Move(float horizontalInput)
		{
			float horizontalInputValue = ClampWalkingInput(horizontalInput);
			_animator.SetFloat("Horizontal", horizontalInputValue);
			transform.position += new Vector3(Speed * horizontalInputValue * Time.deltaTime, 0, 0);
		}

		private float ClampWalkingInput(float horizontalInput)
		{
			return _inputController.Movement.IsWalking ? Mathf.Clamp(horizontalInput, -0.3f, 0.3f) : horizontalInput;
		}

		public void HandleCanMove(bool canTurn)
		{
			CanMove.Value = !canTurn;
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
