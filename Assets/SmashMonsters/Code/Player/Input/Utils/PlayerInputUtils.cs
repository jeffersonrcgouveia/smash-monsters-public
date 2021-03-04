using System.Collections.Generic;
using UnityEngine;

namespace SmashMonsters.Player.Input.Utils
{
	public static class PlayerInputUtils
	{
		/*----------------------------------------------------------------------------------------*
	     * Variables
	     *----------------------------------------------------------------------------------------*/

		private static readonly Dictionary<string, float> _axisStartTime = new Dictionary<string, float>();
    
		/*----------------------------------------------------------------------------------------*
	     * Static Methods
	     *----------------------------------------------------------------------------------------*/

		/**
	     * <summary>
	     * Handle the functionality of press and hold an input button during some period of time
	     * </summary>
	     *
	     * <param name="buttonName">
	     * The virtual button name
	     * </param>
	     *
	     * <param name="onHoldingStart">
	     * Called during the first frame the user pressed down the virtual button identified by buttonName
	     * </param>
	     *
	     * <param name="onHolding">
	     * Called while the virtual button identified by buttonName is held down. This callBack will pass the time passed in
	     * seconds since the callBack "onHoldingStart" was called. Has a boolean return type that represents if the callBack
	     * "onHoldingEnd" can be fired. This callBack will be fired in looping until the buttonName is released or the
	     * value true was returned on this callBack
	     * </param>
	     *
	     * <param name="onHoldingEnd">
	     * Called during the first frame the user releases the virtual button identified by buttonName
	     * </param>
	     */
		public static void HoldButton(string buttonName, ButtonValidateCallBack onHoldingStart, 
			ButtonHoldingCallBack onHolding, ButtonCallBack onHoldingEnd, bool waitClickTimeToFireOnHolding = true)
		{
			if (UnityEngine.Input.GetButtonDown(buttonName))
			{
				bool? canHold = onHoldingStart?.Invoke();
				if (canHold.GetValueOrDefault(true))
				{
					_axisStartTime.Add(buttonName, Time.time);
				}
			}
			else if (UnityEngine.Input.GetButton(buttonName))
			{
				if (!_axisStartTime.TryGetValue(buttonName, out float startTime)) return;
				float holdingSeconds = Time.time - startTime;
				bool canFireOnHolding = !waitClickTimeToFireOnHolding || holdingSeconds >= 0.2f;
				if (canFireOnHolding && onHolding.Invoke(holdingSeconds))
				{
					TryInvokeOnHoldingEnd(buttonName, onHoldingEnd);
				}
			}
			else if (UnityEngine.Input.GetButtonUp(buttonName))
			{
				TryInvokeOnHoldingEnd(buttonName, onHoldingEnd);
			}
		}
	
		public static void HoldAxis(string axisName, AxisComparatorCallBack comparatorCallBack, ButtonValidateCallBack onHoldingStart, 
			ButtonHoldingCallBack onHolding, ButtonCallBack onHoldingEnd, bool waitClickTimeToFireOnHolding = true)
		{
			if (_axisStartTime.TryGetValue(axisName, out float startTime))
			{
				float holdingSeconds = Time.time - startTime;
				bool canFireOnHolding = !waitClickTimeToFireOnHolding || holdingSeconds >= 0.2f;
				if (canFireOnHolding && onHolding.Invoke(holdingSeconds))
				{
					TryInvokeOnHoldingEnd(axisName, onHoldingEnd);
				}
			}
			else if (comparatorCallBack(UnityEngine.Input.GetAxis(axisName)))
			{
				bool? canHold = onHoldingStart?.Invoke();
				if (canHold.GetValueOrDefault(true))
				{
					_axisStartTime.Add(axisName, Time.time);
				}
			}
			else if (UnityEngine.Input.GetAxis(axisName) == 0)
			{
				TryInvokeOnHoldingEnd(axisName, onHoldingEnd);
			}
		}
    
		/*----------------------------------------------------------------------------------------*
	     * Private Static Methods
	     *----------------------------------------------------------------------------------------*/

		private static void TryInvokeOnHoldingEnd(string buttonName, ButtonCallBack onHoldingEnd)
		{
			if (_axisStartTime.Remove(buttonName))
			{
				onHoldingEnd?.Invoke();
			}
		}

		/*----------------------------------------------------------------------------------------*
	     * Inner Classes and Delegates
	     *----------------------------------------------------------------------------------------*/

		public delegate bool AxisComparatorCallBack(float axisValue);

		public delegate bool ButtonValidateCallBack();

		public delegate void ButtonCallBack();

		public delegate bool ButtonHoldingCallBack(float holdingSeconds);

	}
}