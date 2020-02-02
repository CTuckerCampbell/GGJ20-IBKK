﻿using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Singleton
    public static UiManager Instance;
    private void OnEnable() => Instance = this;
    #endregion

    /* Variables */

    [Header("Self References")]
    [SerializeField] private Image newCardShowoffPanel = null;
    [SerializeField] private Image newCardImage = null;
    [SerializeField] private Image noRoomForNewCardImage = null;

    // States
    private bool showingOffNewCard = false;
    private bool showingNoRoomForNewCard = false;

    /* Main Functionality */

    public void ShowOffNewCard(Sprite cardSprite, bool noRoomForNewCard)
    {
        showingOffNewCard = true;
        newCardShowoffPanel.gameObject.SetActive(true);
        newCardImage.sprite = cardSprite;

        if (noRoomForNewCard == false)
        {
            newCardImage.gameObject.SetActive(true);
        }
        else
        {
            showingNoRoomForNewCard = true;
            noRoomForNewCardImage.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (InputListener.Instance.PressedDown_Mouse_LeftClick)
        {
            if (showingNoRoomForNewCard)
                AcknowledgedFullHand();
            else if (showingOffNewCard)
                FinishShowingOffNewCard();
        }
    }

    private void AcknowledgedFullHand()
    {
        showingNoRoomForNewCard = false;
        noRoomForNewCardImage.gameObject.SetActive(false);
        newCardImage.gameObject.SetActive(true);
    }

    private void FinishShowingOffNewCard()
    {
        showingOffNewCard = false;
        newCardImage.gameObject.SetActive(false);
        newCardShowoffPanel.gameObject.SetActive(false);
    }
}