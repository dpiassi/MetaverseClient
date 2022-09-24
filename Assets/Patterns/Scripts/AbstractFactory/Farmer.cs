using UnityEngine;

namespace Patterns.AbstractFactory
{
    public class Farmer : IHuman
    {
        public void Speak()
        {
            Debug.Log("Farmer: You reap what you sow!");
        }
    }
}
