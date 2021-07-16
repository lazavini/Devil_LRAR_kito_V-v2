/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System.Linq;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    public Transform TextTargetName;
    public Transform ButtonAction;
    public Transform TextDescription;
    public Transform PanelDescription;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    
    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);


        
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    void Update()
    {
        if (mTrackableBehaviour == null)
            return;
        var card = TrackedCardsCollection.CardsDataBase.FirstOrDefault(x => x.Name == mTrackableBehaviour.name);
        if (card == null) return;
        card.ChangeCardComponent(mTrackableBehaviour);
        card.CardTrackChanged(mTrackableBehaviour.CurrentStatus);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        
        
        ///Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + 
           //       " " + mTrackableBehaviour.CurrentStatus +
             //     " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            Debug.Log($"FOUND: {mTrackableBehaviour.name}");
            var card = TrackedCardsCollection.CardsDataBase.FirstOrDefault(x => x.Name == mTrackableBehaviour.name);
            if (card == null) return;
            card.ChangeCardComponent(mTrackableBehaviour);
            card.CardTrackChanged(TrackableBehaviour.Status.TRACKED);
            TrackedCardsCollection.AddCard(card);
        }
    }


    protected virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var card = TrackedCardsCollection.CardsDataBase?.FirstOrDefault(x => x.Name == mTrackableBehaviour.name);
            if (card == null) return;
            TrackedCardsCollection.RemoveCard(card);
            card?.CardTrackChanged(TrackableBehaviour.Status.NO_POSE);
        }
        //Evertime the target lost / no target found it will show �???� on the TextTargetName. Button, Description and Panel will invicible (inactive)

        //TextTargetName.GetComponent<Text>().text = "???";
        //ButtonAction.gameObject.SetActive(false);
        //TextDescription.gameObject.SetActive(false);
        //PanelDescription.gameObject.SetActive(false);

    }

    #endregion // PROTECTED_METHODS
}
