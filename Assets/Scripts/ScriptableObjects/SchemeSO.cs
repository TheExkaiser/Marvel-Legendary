using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Scheme")]

public class SchemeSO : ScriptableObject
{
    public string name;
    public string gameSet;
    [ShowAssetPreview] public Sprite image;

    public int numberOfTwists;

    public GameObject schemeAbilities;
    


}
