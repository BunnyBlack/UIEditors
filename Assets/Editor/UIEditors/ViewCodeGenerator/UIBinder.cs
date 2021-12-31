using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor.UIEditors.ViewCodeGenerator
{
    public class UIBinder
    {
        [ShowInInspector, OnValueChanged("ClearAll")]
        private GameObject targetGo;

        [ShowInInspector, HideIf("IsTargetGoNull"),
         InfoBox("请选择该物体上的View脚本，没有则创建一个", InfoMessageType.Info, "IsScriptNull")]
        [ValueDropdown("GetListOfMonoBehavioursOnThisGo"), InlineButton("CreateNewViewScript", "New"), OnValueChanged("GenerateAllComponents")]
        private MonoBehaviour targetScript;

        [ShowInInspector, HideIf("IsScriptNull")]
        private List<UIElement> componentList;

        [ShowInInspector, OnValueChanged("CreateNewUIElement"), HideIf("IsScriptNull"), BoxGroup("新变量")]
        [ValidateInput("CheckIfChildren")]
        [TypeFilter("GetListOfChildren")]
        private GameObject newGo;

        #region Privates

        private void ClearAll()
        {
            targetScript = null;
            componentList = null;
        }

        private bool IsTargetGoNull()
        {
            return targetGo == null;
        }

        private bool IsScriptNull()
        {
            return targetScript == null;
        }

        private IEnumerable<MonoBehaviour> GetListOfMonoBehavioursOnThisGo()
        {
            return targetGo == null ? new MonoBehaviour[]{} : targetGo.GetComponents<MonoBehaviour>();
        }

        private void CreateNewViewScript()
        {
            Debug.Log("Create New");
        }

        private void GenerateAllComponents()
        {
            var elementList = new List<UIElement>();
            
            foreach (var propertyInfo in targetScript.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static))
            {
                var type = UIElement.GetUIElementTypeByType(propertyInfo.FieldType);
                if (type == UIElementType.Unknown)
                {
                    continue;
                }
                var mono = propertyInfo.GetValue(targetScript) as MonoBehaviour;
                var uiElement = new UIElement
                {
                    Obj = mono.gameObject,
                    FieldName = propertyInfo.Name,
                    Type = type
                };
                elementList.Add(uiElement);
            }
            
            componentList = elementList;
            
        }

        private bool CheckIfChildren()
        {
            var list = targetGo.GetComponentsInChildren<Transform>();
            return list.Any(transform => transform == newGo.transform);
        }
        
        private void CreateNewUIElement()
        {
            
        }

        #endregion

    }
}
