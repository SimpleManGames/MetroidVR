using UnityEditor;
using UnityEngine;

namespace Simplicity.Attributes
{
    [CustomPropertyDrawer(typeof(GameObjectNameAttribute))]
    public class GameObjectNameDrawer : PropertyDrawer
    {
        #region Properties

        /// <summary>
        /// Returns the attribute that we are changing -- read only
        /// </summary>
        public GameObjectNameAttribute Target
        {
            get
            {
                return attribute as GameObjectNameAttribute;
            }
        }

        #endregion

        #region Unity Overrides

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Target.stringValue == null || Target.stringValue == string.Empty)
                DrawGameObjectSelection(position);
            else
                DrawCustomStringField(position);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Displays and handles the function of the game object selection
        /// </summary>
        /// <param name="rect">The location of the property supplied by OnGUI's position param</param>
        private void DrawGameObjectSelection(Rect rect)
        {
            EditorGUI.BeginChangeCheck();
            Target.gameObject = EditorGUI.ObjectField(rect, "Get GameObject Name", Target.gameObject, typeof(GameObject), true) as GameObject;
            if (EditorGUI.EndChangeCheck())
                Target.stringValue = Target.gameObject.name;
        }

        /// <summary>
        /// Displays and handles the function of the string field editor
        /// </summary>
        /// <param name="rect">The location of the property supplied by OnGUI's position param</param>
        private void DrawCustomStringField(Rect rect)
        {
            Rect outRect = rect;
            DrawTextField(rect, out outRect);
            DrawClearButton(outRect);
        }

        /// <summary>
        /// Displays and handles the function of the override text box
        /// </summary>
        /// <param name="rect">The location of the property supplied by OnGUI's position param</param>
        /// <param name="outRect">Outputs the edited rect so that we can use it for DrawClearButton</param>
        private void DrawTextField(Rect rect, out Rect outRect)
        {
            outRect = rect;
            outRect.width -= 22;
            if (Target.canOverrideString)
                Target.stringValue = EditorGUI.TextField(outRect, Target.stringValue);
            else
                EditorGUI.TextField(outRect, Target.stringValue);
        }

        /// <summary>
        /// Displays and handles the function of deleting the values the property has stored
        /// </summary>
        /// <param name="stringRect">The output value of DrawTextField function</param>
        private void DrawClearButton(Rect stringRect)
        {
            Rect buttonRect = stringRect;
            buttonRect.width = 20;
            buttonRect.x = stringRect.position.x + stringRect.width + 1;

            if (GUI.Button(buttonRect, "X"))
                Target.NullValues();
        }

        #endregion
    }
}