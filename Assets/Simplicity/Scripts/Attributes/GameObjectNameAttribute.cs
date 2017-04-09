using UnityEngine;

namespace Simplicity.Attributes
{
    public class GameObjectNameAttribute : PropertyAttribute
    {
        #region Varibles

        /// <summary>
        /// This is the GameObject to get the string value of it's name
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// Making this true allows you write into the string field
        /// </summary>
        public bool canOverrideString;

        /// <summary>
        /// Holds the name of the gameObject and will be edited by canOverrideString
        /// </summary>
        public string stringValue = "";

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GameObjectNameAttribute()
        {
            canOverrideString = false;
        }

        /// <summary>
        /// Advance Constructor
        /// </summary>
        /// <param name="overriable">Determines if you will be able to change the value once the gameObject is selected</param>
        public GameObjectNameAttribute(bool overriable)
        {
            canOverrideString = overriable;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Nulls and clears values stored in the attribute
        /// </summary>
        public void NullValues()
        {
            gameObject = null;
            stringValue = "";
        }

        #endregion
    }
}