using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class WheelButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private float degrees;
    [SerializeField] private UnityEvent Selected;

    // Update is called once per frame
    public void OnSelect(BaseEventData eventData)
    {
        Vector3 rotationEuler = Vector3.zero;
        rotationEuler.z = degrees;

        //Spin Wheel container
        transform.parent.DOLocalRotate(rotationEuler, 0.3f).SetEase(Ease.OutBack);

        //Call the event
        Selected.Invoke();

        //Change the last button
        FindObjectOfType<MenuManager>().lastButton = GetComponent<Button>();
    }
}
