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

    [SerializeField] private Transform _menuSpawnLocation;

    public bool _isDisplaying = false;

    private ObjectDisplayView _curView;

    void Start() {
        if(_menuSpawnLocation == null) {
            _menuSpawnLocation = this.gameObject.transform;
        }
    }

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
            if(_curView)
                DeleteDisplay();
            _isDisplaying = false;
        }
    }

    public void CreateItemView() {
        ObjectDisplayView newView = Instantiate(_display, this._menuSpawnLocation);
        newView.SetDisplayWindow(this._objectInfo);
        newView._closeDisplay.onClick.AddListener(ToggleDisplay);

        _curView = newView;
    }

    public void DeleteDisplay() {
        _curView.DeleteDisplay();
    }

}
