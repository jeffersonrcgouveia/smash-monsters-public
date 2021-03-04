using AvoEx.ObservableType;
using UnityEngine;

namespace SmashMonsters.Player.Input.Impl
{
	public class PlayerMovementInput : MonoBehaviour, CharacterMovementInput
	{
		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/
    
		public ObFloat Horizontal { get; } = new ObFloat();
    
		public ObFloat Vertical { get; } = new ObFloat();
    
		public ObFloat LastHorizontal { get; } = new ObFloat();
    
		public ObFloat LastVertical { get; } = new ObFloat();
    
		public ObFloat HorizontalRaw { get; } = new ObFloat();
    
		public ObFloat VerticalRaw { get; } = new ObFloat();

		public ObBool IsJumping { get; } = new ObBool();

		public ObBool IsWalking { get; } = new ObBool();
    
//	private readonly Dictionary<string, HoldingActionInfo> _holdingActionsByActions =
//		new Dictionary<string, HoldingActionInfo>();

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Update()
		{
			IsWalking.Value = UnityEngine.Input.GetButton("Run");

			float horizontalValue = Horizontal.Value;
			float verticalValue = Vertical.Value;

			LastHorizontal.Value = horizontalValue;
			LastVertical.Value = verticalValue;

			Horizontal.Value = UnityEngine.Input.GetAxis("Horizontal");
			Vertical.Value = UnityEngine.Input.GetAxis("Vertical");
		
			HorizontalRaw.Value = UnityEngine.Input.GetAxisRaw("Horizontal");
			VerticalRaw.Value = UnityEngine.Input.GetAxisRaw("Vertical");

			IsJumping.Value = UnityEngine.Input.GetButtonDown("Jump");
		}

		/*----------------------------------------------------------------------------------------*
		 * Methods
		 *----------------------------------------------------------------------------------------*/
	
//	private void HandleHoldingButtons()
//	{
//		foreach (KeyValuePair<string, HoldingActionInfo> pair in _holdingActionsByActions)
//		{
//			HoldingActionInfo info = pair.Value;
//			HoldAxis(pair.Key, 0, info.onHoldingStart, info.onHolding, info.onHoldingEnd,
//				info.waitClickTimeToFireOnHolding);
//		}
//	}
//
//	public void AddHoldingAxis(string axis, ButtonValidateCallBack onHoldingStart,
//		ButtonHoldingCallBack onHolding, ButtonCallBack onHoldingEnd, bool waitClickTimeToFireOnHolding = true)
//	{
//		HoldingActionInfo info;
//		info.onHoldingStart = onHoldingStart;
//		info.onHolding = onHolding;
//		info.onHoldingEnd = onHoldingEnd;
//		info.waitClickTimeToFireOnHolding = waitClickTimeToFireOnHolding;
//        
//		_holdingActionsByActions.Add(axis, info);
//	}
//
//	public bool RemoveHoldingAction(string axis)
//	{
//		return _holdingActionsByActions.Remove(axis);
//	}

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

		// public struct HoldingActionInfo
		// {
		// 	internal ButtonValidateCallBack onHoldingStart;
		// 	internal ButtonHoldingCallBack onHolding;
		// 	internal ButtonCallBack onHoldingEnd;
		// 	internal bool waitClickTimeToFireOnHolding;
		// }
    
	}
}
