using TMPro;
using UnityEngine;

namespace FpsEcs.Runtime.UI.HUD
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _health;

        public void SetHealth(float health)
        {
            _health.text = "Health: " + health;
        }
    }
}