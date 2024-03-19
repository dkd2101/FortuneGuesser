using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectDisplayView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _objectName;

    [SerializeField] TextMeshProUGUI _objectDescription;

    [SerializeField] Image _objectImage;

    [SerializeField] GameObject _window;

    // Set the evidence information to their repsective text objects in the window
    public void SetDisplayWindow(EvidenceSO evidence) {
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.gameObject.GetComponent<RectTransform>());
        _objectName.text = evidence.EvidenceName;
        _objectDescription.text = evidence.Description;
    }

    // Hide the window to the user
    public void HideObjectInfo() {
        this._window.SetActive(false);
    }

    public void DeleteDisplay() {
        Destroy(this.gameObject);
    }
}
