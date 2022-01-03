// 方便编辑用的脚本 请在这边写好以后 将标志设好 并将代码转放到相应的txt文件中

using UnityEngine;

namespace Editor.UIEditors.ViewCodeGenerator.CodeTemplate
{
    public class ViewTemplate : MonoBehaviour
    {
        private bool isAdded;
        // 开始和结束标志之间会被更新重置，编辑器中未持有的字段请不要写在开始和结束标志之间
        // 请不要改动开始结束标志，否则将无法再支持更新操作
        // FieldStart
        // FieldEnd

        private void Start()
        {
            AddOrRemoveListeners(true);
        }

        private void AddOrRemoveListeners(bool add)
        {
            if (!isAdded && add)
            {
                isAdded = true;
                
                // RegisterStart
                // RegisterEnd
            }
            else if (!add && isAdded)
            {
                isAdded = false;
                
                // UnregisterStart
                // UnregisterEnd
            }
        }

        private void OnDestroy()
        {
            AddOrRemoveListeners(false);
        }
    }
}
