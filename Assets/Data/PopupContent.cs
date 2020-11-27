using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/PopupContent", order = 1)]
public class PopupContent : ScriptableObject
{
    public Sprite popupImage;
    public string titre;
    public string contenu;
}