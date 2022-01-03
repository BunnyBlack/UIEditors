﻿using System.Collections.Generic;
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
        [ValueDropdown("GetListOfMonoBehavioursOnThisGo"), InlineButton("CreateNewViewScript", "New"),
         OnValueChanged("GenerateAllComponents")]
        private Behaviour targetScript;

        [ShowInInspector, HideIf("IsScriptNull")] [ListDrawerSettings(HideAddButton = true)]
        private List<UIElement> componentList;

        [InfoBox("放入的物体必须是targetGo的子物体", "@newGoError", InfoMessageType = InfoMessageType.Error)]
        [ShowInInspector, OnValueChanged("CreateNewUIElement"), HideIf("IsScriptNull"), BoxGroup("新变量")]
        private GameObject newGo;

#pragma warning disable CS0414
        private bool newGoError;
#pragma warning restore CS0414
        

        [Button(ButtonSizes.Large), ButtonGroup("GenerateGroup"), HideIf("IsScriptNull")]
        private void GenerateCode()
        {
            Debug.Log($"生成脚本: {targetScript.name}");
        }

        [Button(ButtonSizes.Large), ButtonGroup("GenerateGroup"), HideIf("IsScriptNull")]
        private void UpdateCode()
        {
            Debug.Log($"更新脚本: {targetScript.name}");
        }

        #region Public

        #endregion

        #region Private

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

        private IEnumerable<Behaviour> GetListOfMonoBehavioursOnThisGo()
        {
            return targetGo == null ? new Behaviour[] { } : targetGo.GetComponents<Behaviour>();
        }

        private void CreateNewViewScript()
        {
            Debug.Log("Create New");
        }

        private void GenerateAllComponents()
        {
            var elementList = new List<UIElement>();

            foreach (var propertyInfo in targetScript.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance |
                                                                          BindingFlags.Public | BindingFlags.DeclaredOnly |
                                                                          BindingFlags.Static))
            {
                var type = propertyInfo.FieldType.ToString();
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
            if (newGo == null)
            {
                return true;
            }
            var list = targetGo.GetComponentsInChildren<Transform>();
            return list.Any(transform => transform == newGo.transform);
        }

        private void CreateNewUIElement()
        {
            if (!CheckIfChildren())
            {
                newGoError = true;
                newGo = null;
                return;
            }
            newGoError = false;
            
            var uiElement = new UIElement
            {
                Obj = newGo
            };
            uiElement.UpdateFieldName();
            componentList.Add(uiElement);
            newGo = null;
        }

        #endregion

    }
}
