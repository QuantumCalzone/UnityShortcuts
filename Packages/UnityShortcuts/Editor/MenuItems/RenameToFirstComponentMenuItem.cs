#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityShortcuts
{
    public static class RenameToFirstComponentMenuItem
    {
        private const string menuItemItemName = "Tools/Rename To First Component";
        private const int menuItemPriority = 9999;

        [MenuItem(itemName: menuItemItemName, isValidateFunction: false, priority: menuItemPriority)]
        public static void RenameToFirstComponent()
        {
            var selectedGameObjects = Selection.gameObjects;

            for (var i = 0; i < selectedGameObjects.Length; i++)
            {
                var selectedGameObject = selectedGameObjects[i];

                var components = selectedGameObject.GetComponents(typeof(Component));

                if (components.Length > 1)
                {
                    selectedGameObject.name = components[1].GetType().Name;
                    EditorUtility.SetDirty(selectedGameObject);
                }
            }
        }

        [MenuItem(itemName: menuItemItemName, isValidateFunction: true, priority: menuItemPriority)]
        public static bool RenameToFirstComponentValidator()
        {
            var selectedGameObjects = Selection.gameObjects;

            if (selectedGameObjects.Length == 0)
            {
                return false;
            }

            for (var i = 0; i < selectedGameObjects.Length; i++)
            {
                var selectedGameObject = selectedGameObjects[i];
                var components = selectedGameObject.GetComponents(typeof(Component));

                if (components.Length > 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
#endif
