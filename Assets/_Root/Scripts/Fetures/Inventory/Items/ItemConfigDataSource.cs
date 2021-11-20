using UnityEngine;
using System.Collections.Generic;

namespace Features.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemConfigDataSource), menuName = "Configs/" + nameof(ItemConfigDataSource))]
    internal class ItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _items;

        public IReadOnlyList<ItemConfig> ItemConfigs => _items;
    }
}
