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
	[ SerializeField ] Sprite sprite_purchase_button_active;
	[ SerializeField ] Sprite sprite_purchase_button_deactive;

  [ Title( "Components" ) ]
    [ SerializeField ] Button button_purchase;
    [ SerializeField ] Image button_purchase_image;
    [ SerializeField ] Image image_purchase_icon_current;
    [ SerializeField ] Image image_purchase_icon_next;
    [ SerializeField ] Image image_purchase_progress;
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
		UpdatePurchaseContext();
		GoToTarget( EnableInteraction );
	}

    public void OnLevelFinished()
    {
		GoToStart( DisableInteraction );
	}

    public void OnButtonPress()
    {
		event_purchase.Raise();

		var cost = system_purchase.GetPurchaseCost();
		system_purchase.IncreasePurchaseCount();

		currency.SharedValue -= cost;
		UpdatePurchaseContext();
	}

	public void OnCurrencyUpdate()
	{
		UpdatePurchaseText();
		TogglePurchaseButton();
	}

	public void TogglePurchaseButton()
	{
		var enabled = notif_table_empty.sharedValue && system_purchase.GetPurchaseCost() <= currency.sharedValue;

		button_purchase.interactable = enabled;

		if( enabled )
			button_purchase_image.sprite = sprite_purchase_button_active;
		else
			button_purchase_image.sprite = sprite_purchase_button_deactive;
	}
#endregion

#region Implementation
	void UpdatePurchaseText()
	{
		text_purchase_cost.text            = currency.sharedValue.ToString( "F1" );
		text_purchase_count.text           = system_purchase.PurchaseCount + " / " + system_purchase.PurchaseCeil;
		image_purchase_progress.fillAmount = Mathf.InverseLerp( system_purchase.GetPurchaseFloor(), system_purchase.PurchaseCeil, system_purchase.PurchaseCount );
	}

	void UpdatePurchaseContext()
	{
		image_purchase_icon_current.sprite = system_purchase.GetPurchaseContext();
		image_purchase_icon_next.sprite    = system_purchase.GetNextPurchaseContext();
	}

	void EnableInteraction()
	{
		button_purchase.interactable = true;
	}

	void DisableInteraction()
	{
		button_purchase.interactable = false;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}