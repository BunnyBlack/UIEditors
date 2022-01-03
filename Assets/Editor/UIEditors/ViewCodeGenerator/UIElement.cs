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
        [OnValueChanged("UpdateFieldName")] public string FieldName;

        [HorizontalGroup("Type Group")]
        [ValueDropdown("GetListOfUIElementType"), OnValueChanged("CheckIfUnknown"), VerticalGroup("Type Group/Type")]
        public string Type = UIElementType.Unknown;

        private bool showTypeInputArea = false;

        [ShowIf("@showTypeInputArea"), VerticalGroup("Type Group/TypeName")]
        [LabelWidth(70)]
        public string TypeName;

        #region Public

        #endregion

        #region Private

        private void UpdateFieldName()
        {
            FieldName = Obj.name;
        }

        private IEnumerable<string> GetListOfUIElementType()
        {
            if (Obj == null)
            {
                return new string[] { };
            }
            var list = Obj.GetComponents<Behaviour>();
            var components = new List<string>();
            foreach (var behaviour in list)
            {
                var type = GetUIElementTypeByType(behaviour.GetType());
                if (type != UIElementType.Unknown)
                {
                    components.Add(type);
                }
            }
            components.Add(UIElementType.Unknown);
            return components;
        }

        private void CheckIfUnknown()
        {
            showTypeInputArea = Type == UIElementType.Unknown;
        }


        public static string GetUIElementTypeByType(Type type)
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
                return UIElementType.Scrollbar;
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

    }
}
