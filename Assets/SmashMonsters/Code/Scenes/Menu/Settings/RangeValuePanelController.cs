using AvoEx.ObservableType;
using Plugins.MinMaxSlider;
using TMPro;
using UnityEngine;

namespace SmashMonsters.Scenes.Menu.Settings
{
	public class RangeValuePanelController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
	     * SerializeField
	     *----------------------------------------------------------------------------------------*/

		[SerializeField]
		private TextMeshProUGUI text;

		[SerializeField]
		[MinMaxSlider(0f, 1000f)]
		private MinMax valueLimits = new MinMax(0f, 1000f);

		[SerializeField]
		private string suffix;
	
		/*----------------------------------------------------------------------------------------*
	     * Attributes
	     *----------------------------------------------------------------------------------------*/

		public ObInt Value = new ObInt();
	
		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			text.text = FormatValue(Value.Value);
			Value.AddObserver(value =>
			{
				text.text = FormatValue(Value);
			});
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		public void PreviewsValue()
		{
			if (Value > valueLimits.Min)
			{
				Value--;
			}
		}
	
		public void NextValue()
		{
			if (Value < valueLimits.Max)
			{
				Value++;
			}
		}
	
		/*----------------------------------------------------------------------------------------*
	     * Utility Methods
	     *----------------------------------------------------------------------------------------*/

		private string FormatValue(int value)
		{
			return suffix != null ? value + " " + suffix : value.ToString();
		}

	}
}
