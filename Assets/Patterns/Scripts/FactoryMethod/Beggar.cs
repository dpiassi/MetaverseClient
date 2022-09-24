using UnityEngine;

namespace Patterns.FactoryMethod
{
    public class Beggar : INPC
    {
        public void Speak()
        {
            Debug.Log("Beggar: Do you have some change to spare?");
        }
    }
}
