using AvoEx.ObservableType;
using TopDownMedieval.Plugins.Commons.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace SmashMonsters.Scenes.Stages.BaseStage
{
	public class CharacterLivesPanelController : MonoBehaviour
	{
		/*----------------------------------------------------------------------------------------*
		 * Constants
		 *----------------------------------------------------------------------------------------*/

		private const string LifeGOName = "Life";

		private const string LifeAmountGOName = "Amount";

		private const string LifeAmountPanelName = "LifeAmountPanel";

		private const int AmountObjectsLimit = 5;

		/*----------------------------------------------------------------------------------------*
		 * SerializeField
		 *----------------------------------------------------------------------------------------*/

		[field:SerializeField]
		public ObInt Amount { get; set; }

		[SerializeField]
		private Sprite liveSprite;

		/*----------------------------------------------------------------------------------------*
	     * Events
	     *----------------------------------------------------------------------------------------*/

		private void Awake()
		{
			Amount.AddObserver(target =>
			{
				/*
			 * That was the solution to the exception: UnityException: Method() is not allowed to be called during
			 * serialization, call it from Awake or Start instead.
			 */
				ThreadDispatcher.RunOnMainThread(() =>
				{
					RebuildChildren();
					transform.parent.RebuildLayoutImmediate();
				});
			});

			RebuildChildren(false);
			transform.parent.RebuildLayoutImmediate();
		}

		/*----------------------------------------------------------------------------------------*
	     * Methods
	     *----------------------------------------------------------------------------------------*/

		private void RebuildChildren(bool clearChildren = true)
		{
			if (clearChildren)
			{
				transform.DetachChildren();
			}

			if (Amount <= AmountObjectsLimit)
			{
				for (int i = 0; i < Amount; i++)
				{
					GameObject imageGO = CreateLife();
					imageGO.transform.SetParent(transform);
				}
			}
			else
			{
				GameObject lifeAmountPanel = CreateLifeAmount();
				lifeAmountPanel.transform.SetParent(transform);
				lifeAmountPanel.transform.RebuildLayoutImmediate();
			}
		}

		private GameObject CreateLife()
		{
			GameObject imageGO = new GameObject(LifeGOName);
			Image image = imageGO.AddComponent<Image>();
			image.sprite = liveSprite;
			image.preserveAspect = true;
			image.SetNativeSize();
			return imageGO;
		}

		private GameObject CreateLifeAmount()
		{
			GameObject lifeGO = CreateLife();

			GameObject textGO = new GameObject(LifeAmountGOName);
			Text text = textGO.AddComponent<Text>();
			Font arialFont = (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			text.font = arialFont;
			text.fontSize = 60;
			text.material = arialFont.material;
			text.text = "x" + Amount.Value;
			ContentSizeFitter textFitter = textGO.AddComponent<ContentSizeFitter>();
			textFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			textFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

			GameObject lifeAmountPanel = new GameObject(LifeAmountPanelName);
			HorizontalLayoutGroup layoutGroup = lifeAmountPanel.AddComponent<HorizontalLayoutGroup>();
			layoutGroup.spacing = 10f;
			layoutGroup.childControlWidth = false;
			layoutGroup.childControlHeight = false;
			ContentSizeFitter fitter = lifeAmountPanel.AddComponent<ContentSizeFitter>();
			fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
			fitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

			lifeGO.transform.SetParent(lifeAmountPanel.transform);
			textGO.transform.SetParent(lifeAmountPanel.transform);

			TransformUtils.ResetLocalTransform(lifeAmountPanel.transform);

			return lifeAmountPanel;
		}
	}
}
