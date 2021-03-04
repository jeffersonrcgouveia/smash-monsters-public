using AvoEx.ObservableType;
using SmashMonsters.Player.Input.Utils;
using UnityEngine;
using static SmashMonsters.Player.Input.Utils.PlayerInputUtils;

namespace SmashMonsters.Player.Input.Impl
{
	public class CPUMovementInput : MonoBehaviour, CharacterMovementInput
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
			IsWalking.Value = false;

			float horizontalValue = Horizontal.Value;
			float verticalValue = Vertical.Value;

			LastHorizontal.Value = horizontalValue;
			LastVertical.Value = verticalValue;

			Horizontal.Value = 0;
			Vertical.Value = 0;
		
			HorizontalRaw.Value = 0;
			VerticalRaw.Value = 0;

			IsJumping.Value = false;
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

		public struct HoldingActionInfo
		{
			internal PlayerInputUtils.ButtonValidateCallBack onHoldingStart;
			internal PlayerInputUtils.ButtonHoldingCallBack onHolding;
			internal PlayerInputUtils.ButtonCallBack onHoldingEnd;
			internal bool waitClickTimeToFireOnHolding;
		}
    
	}
}
