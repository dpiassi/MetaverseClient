using UnityEngine;

namespace Patterns.MVC
{
    /// <summary>
    /// USER INTERFACE: represents current Model state.
    /// </summary>
    public abstract class View : MonoBehaviour
    {
        /* ============================================================
         * ABSTRACT FUNCTIONS
         * ============================================================*/
        internal abstract void SetupUserInterface(Model model);
        internal abstract void UpdateUserInterface(Model model);
    }
}
