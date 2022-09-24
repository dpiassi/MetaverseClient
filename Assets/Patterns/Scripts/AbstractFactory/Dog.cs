using UnityEngine;

namespace Patterns.AbstractFactory
{
    public class Dog : IAnimal
    {
        public void Voice()
        {
            Debug.Log("Dog: Woof!");
        }
    }
}
