using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

namespace Editor.UIEditors.ViewCodeGenerator
{
    public class UIElement
    {
        public GameObject Obj;
        public string FieldName;
        [TypeFilter("FilterValidType")] public UIElementType Type;

        #region Public

        public static UIElementType GetUIElementTypeByType(Type type)
        {
            if (type == typeof(Text))
            {
                return UIElementType.Text;
            }
            if (type == typeof(Image))
            {
                return UIElementType.Image;
            }
            if (type == typeof(Button))
            {
                return UIElementType.Button;
            }
            if (type == typeof(Toggle))
            {
                return UIElementType.Toggle;
            }
            if (type == typeof(Slider))
            {
                return UIElementType.Slider;
            }
            if (type == typeof(Scrollbar))
            {
                return UIElementType.ScrollBar;
            }
            if (type == typeof(Dropdown))
            {
                return UIElementType.Dropdown;
            }
            if (type == typeof(InputField))
            {
                return UIElementType.InputField;
            }
            if (type == typeof(Canvas))
            {
                return UIElementType.Canvas;
            }
            if (type == typeof(ScrollView))
            {
                return UIElementType.ScrollView;
            }

            return UIElementType.Unknown;
        }

        #endregion

        #region Private

        private IEnumerable<UIElementType> FilterValidType()
        {
            if (Obj == null)
            {
                return new List<UIElementType>();
            }
            var filteredList = new List<UIElementType>();

            var components = Obj.GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                var type = GetUIElementTypeByType(component.GetType());
                if (type != UIElementType.Unknown)
                {
                    filteredList.Add(type);
                }
            }
            return filteredList;
        }

        #endregion

    }
}
