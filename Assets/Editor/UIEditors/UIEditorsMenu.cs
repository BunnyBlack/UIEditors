using Editor.UIEditors.ViewCodeGenerator;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Editor.UIEditors
{
    public class UIEditorsMenu : OdinMenuEditorWindow
    {
        [MenuItem("程序工具/UI工具/UIEditors")]
        private static void ShowWindow()
        {
            GetWindow<UIEditorsMenu>().Show();
        }

        private UIBinder uiBinder;

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            uiBinder = new UIBinder();
            tree.Add("UIBinder", uiBinder);
            return tree;
        }
    }
}
