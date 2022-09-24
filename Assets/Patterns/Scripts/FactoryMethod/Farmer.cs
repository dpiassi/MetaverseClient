using UnityEngine;

namespace Patterns.FactoryMethod
{
    public class Farmer : INPC
    {
        public void Speak()
        {
            Debug.Log("Farmer: You reap what you sow!");
        }
    }
}
