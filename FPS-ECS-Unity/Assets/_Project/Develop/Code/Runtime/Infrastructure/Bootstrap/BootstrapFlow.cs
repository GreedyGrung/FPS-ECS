using UnityEngine;
using VContainer.Unity;

namespace FpsEcs.Runtime.Infrastructure.Bootstrap
{
    public class BootstrapFlow : IStartable
    {
        public void Start()
        {
            Debug.Log("start!");
        }
    }
}