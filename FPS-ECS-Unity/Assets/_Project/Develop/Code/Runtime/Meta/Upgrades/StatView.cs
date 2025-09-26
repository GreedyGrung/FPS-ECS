using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FpsEcs.Runtime.Meta.Upgrades
{
    public class StatView : MonoBehaviour
    {
        public event Action OnAddPointButtonClicked;
        
        [SerializeField] private Button _addPointButton;
        [SerializeField] private TextMeshProUGUI _pendingPoints;
        
        public int PendingPoints { get; private set; }

        public void Initialize(int availablePoints)
        {
            _pendingPoints.text = PendingPoints.ToString();
            _addPointButton.onClick.AddListener(AddPoint);

            _addPointButton.interactable = availablePoints > 0;
        }

        public void Dispose()
        {
            PendingPoints = 0;
            _addPointButton.onClick.RemoveListener(AddPoint);
        }

        public void DisableButton()
        {
            _addPointButton.interactable = false;
        }

        private void AddPoint()
        {
            PendingPoints++;
            _pendingPoints.text = PendingPoints.ToString();
            OnAddPointButtonClicked?.Invoke();
        }
    }
}