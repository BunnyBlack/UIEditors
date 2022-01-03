using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

namespace Editor.UIEditors.ViewCodeGenerator
{
    public static class UIElementType
    {
        public const string Unknown = "Unknown";
        public const string Text = "UnityEngine.UI.Text";
        public const string Image = "UnityEngine.UI.Image";
        public const string Button = "UnityEngine.UI.Button";
        public const string Toggle = "UnityEngine.UI.Toggle";
        public const string Slider = "UnityEngine.UI.Slider";
        public const string Scrollbar = "UnityEngine.UI.Scrollbar";
        public const string Dropdown = "UnityEngine.UI.Dropdown";
        public const string InputField = "UnityEngine.UI.InputField";
        public const string Canvas = "UnityEngine.Canvas";
        public const string ScrollView = "UnityEngine.UIElements.ScrollView";
        
        public static string GetUIElementTypeByType(Type type)
        {
            if (type == typeof(Text))
            {
                return Text;
            }
            if (type == typeof(Image))
            {
                return Image;
            }
            if (type == typeof(Button))
            {
                return Button;
            }
            if (type == typeof(Toggle))
            {
                return Toggle;
            }
            if (type == typeof(Slider))
            {
                return Slider;
            }
            if (type == typeof(Scrollbar))
            {
                return Scrollbar;
            }
            if (type == typeof(Dropdown))
            {
                return Dropdown;
            }
            if (type == typeof(InputField))
            {
                return InputField;
            }
            if (type == typeof(Canvas))
            {
                return Canvas;
            }
            if (type == typeof(ScrollView))
            {
                return ScrollView;
            }

            return Unknown;
        }

        public static Type GetTypeByUIElementType(string uiElementType)
        {
            var uiAssembly = Assembly.Load("UnityEngine.UI");
            return uiAssembly.GetType(uiElementType);
        }
    }
}
