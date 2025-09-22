using UnityEngine;
using VContainer.Unity;

namespace FpsEcs
{
    public class BootstrapFlow : IStartable
    {
        public void Start()
        {
            Debug.Log("start!");
        }
    }
}