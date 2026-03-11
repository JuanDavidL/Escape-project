using UnityEngine;

public enum itemTypeEnum { Municion, Llave, Escudo, Arma, none }

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    [SerializeField] public string _itemName;
    [SerializeField] public Sprite _icon;
   // [SerializeField] public int _spaceSlots;
    [SerializeField] public itemTypeEnum _type;
    [SerializeField] public GameObject _prefab;
/*
    public string itemName { get => itemName; set => itemName = value; }
    public Sprite icon { get => icon; set => icon = value; }
    public int spaceSlots { get => spaceSlots; set => this.spaceSlots = value; }
    public itemTypeEnum type { get => type; set => type = value; }
    public itemTypeEnum prefab { get => prefab; set => prefab = value; }
*/


}
