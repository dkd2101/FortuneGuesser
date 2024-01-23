using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private EvidenceSO _objectInfo;

    [SerializeField] private ObjectDisplayView _display;

    [SerializeField] private RectTransform _clickArea;

    private bool _isDisplaying = false;

    private ObjectDisplayView _curView;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        if(RectTransformUtility.RectangleContainsScreenPoint(_clickArea, eventData.pointerPress.transform.position)) {
            Debug.Log("contains point");
            ToggleDisplay();
        }
    }

    public void ToggleDisplay() {
        if(!_isDisplaying){
            CreateItemView();
            _isDisplaying = true;
        } else {
            DeleteDisplay();
            _isDisplaying = false;
        }
    }

    public void CreateItemView() {
        ObjectDisplayView newView = Instantiate(_display, this.gameObject.transform);
        newView.SetDisplayWindow(this._objectInfo);
        RectTransform displayRect = newView.GetComponent<RectTransform>();
        float newX; 
        float newY;
        if(newView.transform.position.x < Screen.width/2){
            newX = newView.transform.position.x - displayRect.rect.width;
            
        } else {
            newX = newView.transform.position.x + displayRect.rect.width;
        }
        if(newView.transform.position.y < Screen.height / 2) {
            newY = newView.transform.position.y - displayRect.rect.height;
        } else {
            newY = newView.transform.position.y + displayRect.rect.height;
        }

        Debug.Log(newX);
        Debug.Log(newY);
        newView.transform.localPosition = new Vector3(newX, newY);

        _curView = newView;
    }

    public void DeleteDisplay() {
        Debug.Log("deletion");
        _curView.DeleteDisplay();
    }

}
