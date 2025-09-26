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

        private int _usedPoints;
        
        public int PendingPoints { get; private set; }
        
        private int PointsToShow => _usedPoints + PendingPoints;

        public void Initialize(int availablePoints, int level)
        {
            _usedPoints = level;
            _pendingPoints.text = PointsToShow.ToString();
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
            _pendingPoints.text = PointsToShow.ToString();
            OnAddPointButtonClicked?.Invoke();
        }
    }
}