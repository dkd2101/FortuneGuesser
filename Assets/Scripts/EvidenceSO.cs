using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Evidence")]
public class EvidenceSO : ScriptableObject
{
    [SerializeField] public  string EvidenceName;

    [SerializeField] public  string Description;

    [SerializeField] public  Sprite EvidenceImage;

}
