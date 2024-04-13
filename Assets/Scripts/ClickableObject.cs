using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private EvidenceSO _objectInfo;

    [SerializeField] private ObjectDisplayView _display;

    [SerializeField] private RectTransform _clickArea;

    [SerializeField] private Transform _menuSpawnLocation;

    [SerializeField] private ParticleSystem _inSceneParticles;

    public bool _isDisplaying = false;

    private ObjectDisplayView _curView;

    public bool _isInteractable = true;

    void Start() {
        if(_menuSpawnLocation == null) {
            _menuSpawnLocation = this.gameObject.transform;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(_isInteractable)
            ToggleDisplay();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if(_isInteractable)
            ToggleDisplay();
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

        _curView = newView;
    }

    public void DeleteDisplay() {
        _curView.DeleteDisplay();
    }

    public void SetInteractivity(SceneStateManager.Mode mode) {
        if(mode == SceneStateManager.Mode.Observer) {
            _isInteractable = true;
            if(this._inSceneParticles != null) {
                this._inSceneParticles.Play();
            }
        } else {
            _isInteractable = false;
            if(this._inSceneParticles != null) {
                this._inSceneParticles.Stop();
            }
        }
    }
}
