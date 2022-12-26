/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FFStudio;
using TMPro;
using Sirenix.OdinInspector;

public class UIRopeBoxPurchase : UIEntity
{
#region Fields
  [ Title( "Setup" ) ]
	[ SerializeField ] PurchaseSystem system_purchase;
	[ SerializeField ] Currency currency;
	[ SerializeField ] SharedBoolNotifier notif_table_empty;
	[ SerializeField ] GameEvent event_purchase;

  [ Title( "Components" ) ]
    [ SerializeField ] Button button_purchase;
    [ SerializeField ] Image image_purchase_icon;
    [ SerializeField ] Image image_purchase_icon_active;
    [ SerializeField ] Image image_purchase_icon_deactive;
    [ SerializeField ] TextMeshProUGUI text_purchase_cost; // This is actually currency
    [ SerializeField ] TextMeshProUGUI text_purchase_count;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnLevelStarted()
    {
		OnCurrencyUpdate();

		image_purchase_icon.sprite = system_purchase.GetPurchaseContext();
		GoToTarget();
	}

    public void OnLevelFinished()
    {
		GoToStart();
	}

    public void OnButtonPress()
    {
		event_purchase.Raise();

		var cost = system_purchase.GetPurchaseCost();
		system_purchase.IncreasePurchaseCount();

		currency.SharedValue -= cost;
		image_purchase_icon.sprite = system_purchase.GetPurchaseContext();
	}

	public void OnCurrencyUpdate()
	{
		UpdatePurchaseText();
		TogglePurchaseButton();
	}

	public void TogglePurchaseButton()
	{
		var enabled = notif_table_empty.sharedValue && system_purchase.GetPurchaseCost() <= currency.sharedValue;

		button_purchase.interactable         = enabled;
		image_purchase_icon_active.enabled   = enabled;
		image_purchase_icon_deactive.enabled = !enabled;
	}
#endregion

#region Implementation
	void UpdatePurchaseText()
	{
		text_purchase_cost.text  = currency.sharedValue.ToString( "F1" );
		text_purchase_count.text = system_purchase.PurchaseCount + " / " + system_purchase.PurchaseCeil;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}