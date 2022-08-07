using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Assets/Data/Cards/New Card", menuName = "Akelarre/New Card", order = 0)]
public class Cards : ScriptableObject 
{
#region Enums
    public enum EffectTypes
    {
        Self,
        Single,
        Multiple,
        Select
    }
#endregion


    [SerializeField]
    private string _description;
    public string Description
    {
        get { return _description; }
    }

    [SerializeField]
    private Sprite _image;
    public Sprite Image
    {
        get { return _image; }
    }

    [SerializeField]
    private ICardEffects _effectType;
    public ICardEffects EffectType
    {
        get { return _effectType; }
    }
}
