using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UelpSystem : MonoBehaviour
{
    public static UelpReviewSO _featuredReview;

    [SerializeField] private UelpReviewSO _defaultReview;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _reviewField;
    [SerializeField] private Transform _starSpawn;
    [SerializeField] private GameObject _star;

    void Start()
    {
        if (_featuredReview == null)
        {
            _featuredReview = _defaultReview;
        }

        _nameField.text = _featuredReview.OPName;
        _reviewField.text = _featuredReview.review;

        for (int i = 0; i < _featuredReview.starCount; i++)
        {
            Instantiate(_star, _starSpawn);
        }
    }
}
