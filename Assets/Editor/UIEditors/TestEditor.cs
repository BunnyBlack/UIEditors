using Editor.UIEditors.ViewCodeGenerator;
using UnityEditor;
using UnityEngine;
using Assembly = System.Reflection.Assembly;

namespace Editor.UIEditors
{
    public class TestEditor
    {
        [MenuItem("Test/Test1")]
        private static void Test1()
        {
            var selections = Selection.gameObjects;
            var type = UIElementType.GetTypeByUIElementType(UIElementType.Text);
            foreach (var gameObject in selections)
            {
                Undo.AddComponent(gameObject, type);
            }
        }

        [MenuItem("Test/Test2")]
        private static void Test2()
        {
            var assembly = Assembly.Load("UnityEngine.UI");
            Debug.Log(assembly.GetType("UnityEngine.UI.Text"));
        }
    }
}
