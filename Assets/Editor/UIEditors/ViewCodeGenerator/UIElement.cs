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
        [Required]
        public GameObject Obj;
        [OnValueChanged("UpdateFieldName")] public string FieldName;
        [ValueDropdown("GetListOfUIElementType")]
        public string Type = UIElementType.Unknown;
        
        #region Public
        
        public void UpdateFieldName()
        {
            FieldName = Obj.name;
        }
        
        #endregion

        #region Private

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
                var type = UIElementType.GetUIElementTypeByType(behaviour.GetType());
                if (type != UIElementType.Unknown)
                {
                    components.Add(type);
                }
            }
            components.Add(UIElementType.Unknown);
            return components;
        }
        

        #endregion

    }
}
