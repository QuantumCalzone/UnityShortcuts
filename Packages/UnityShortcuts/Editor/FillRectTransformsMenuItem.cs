using UnityEditor;
using UnityEngine;

namespace George
{
    public static class FillRectTransformsMenuItem
    {
        private const string menuItemItemName = "Tools/UI/Rect Transform Fill %#r";
        private const int menuItemPriority = 9999;

        [MenuItem(itemName: menuItemItemName, isValidateFunction: false, priority: menuItemPriority)]
        public static void RectTransformFill()
        {
            var selectedGameObjects = Selection.gameObjects;

            for(var i = 0; i < selectedGameObjects.Length; i++)
            {
                var rectTransform = selectedGameObjects[i].GetComponent<RectTransform>();

                if(rectTransform)
                {
                    if(!IsFilled(rectTransform))
                    {
                        var isInScene = rectTransform.gameObject.scene.name != null;

                        if(isInScene)
                        {
                            Undo.RecordObject(rectTransform, string.Format("{0}RectTransformFill", rectTransform.GetInstanceID()));
                        }

                        rectTransform.anchorMin = Vector2.zero;
                        rectTransform.anchorMax = Vector2.one;
                        rectTransform.anchoredPosition = Vector2.zero;
                        rectTransform.sizeDelta = Vector2.zero;
                        rectTransform.pivot = Vector2.one * 0.5f;

                        if(isInScene)
                        {
                            // I think Undo.RecordObject marks the scene dirty already...
                            //EditorSceneManager.MarkSceneDirty();
                        }
                        else
                        {
                            EditorUtility.SetDirty(rectTransform);
                        }

                        Debug.Log(string.Format("Filled {0} | isInScene: {1}", rectTransform.name, isInScene), rectTransform);
                    }
                }
            }
        }

        [MenuItem(itemName: menuItemItemName, isValidateFunction: true, priority: menuItemPriority)]
        public static bool RectTransformFillValidator()
        {
            var selectedGameObjects = Selection.gameObjects;

            var hasRectTransform = false;

            for(var i = 0; i < selectedGameObjects.Length; i++)
            {
                var rectTransform = selectedGameObjects[i].GetComponent<RectTransform>();
                if(rectTransform)
                {
                    hasRectTransform = true;
                    break;
                }
            }

            //var hasParent = false;

            //for (var i = 0; i < selectedGameObjects.Length; i++)
            //{
            //    if (selectedGameObjects[i].transform.parent != null)
            //    {
            //        hasParent = true;
            //        break;
            //    }
            //}

            //return hasRectTransform & hasParent;
            return hasRectTransform;
        }

        private static bool IsFilled(RectTransform rectTransform)
        {
            return rectTransform.anchorMin != Vector2.zero &&
                        rectTransform.anchorMax != Vector2.one &&
                        rectTransform.anchoredPosition != Vector2.zero &&
                        rectTransform.sizeDelta != Vector2.zero &&
                        rectTransform.pivot != (Vector2.one * 0.5f);
        }
    }
}
