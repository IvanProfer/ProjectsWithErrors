using Tool;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Features.Inventory.Items
{
    internal interface IItemView
    {
        void Init(IItem item, UnityAction clickAction);
        void Deinet();

        void Select();
        void Unselect();
    }

    internal class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private CustomText _text;
        [SerializeField] private Button _button;

        [SerializeField] private GameObject _selectedBackground;
        [SerializeField] private GameObject _unscelectedBackground;

        private string _itemId = string.Empty;

        


        public void Init(IItem item, UnityAction clickAction)
        {
            _itemId = item.Id;
            _text.Text = item.Info.Title;
            _icon.sprite = item.Info.Icon;
            _button.onClick.AddListener(clickAction);
        }

        public void Deinit()
        {
            _text.Text = string.Empty;
            _itemId = string.Empty;
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
        }

        public void Select() => SetSelected(true);

        public void Unselect() => SetSelected(false);

        private void SetSelected(bool selected)
        {
            _selectedBackground.SetActive(selected);
            _selectedBackground.SetActive(!selected);
        }

        public void Deinet()
        {
            
        }

        
    }
}
