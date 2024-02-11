using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UelpReview", menuName = "UelpReview")]
public class UelpReviewSO : ScriptableObject
{
    public string OPName;

    [TextArea(1, 10)]
    public string review;

    public int starCount;

}
