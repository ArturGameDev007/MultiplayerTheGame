using _Project.Scripts.Services.PhotonFusion;
using Fusion;
using UnityEngine;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuPresenter : MonoBehaviour
    {
        [SerializeField] private FusionConnector _fusionConnector;
        [SerializeField] private StartMenuView _startMenuView;
        [SerializeField] private CreatedRoomView _createdRoomView;

        private void Start()
        {
            if (_startMenuView == null)
                return;

            _startMenuView.CreateRoomButton.onClick.AddListener(OnCreateRoomButtonClicked);
            _startMenuView.JoinRoomButton.onClick.AddListener(OnJoinButtonClicked);
        }

        private void OnDestroy()
        {
            if (_startMenuView != null)
            {
                _startMenuView.CreateRoomButton.onClick.RemoveListener(OnCreateRoomButtonClicked);
                _startMenuView.JoinRoomButton.onClick.RemoveListener(OnJoinButtonClicked);
            }
        }

        private void OnCreateRoomButtonClicked()
        {
            int minValue = 1000;
            int maxValue = 9999;

            string randomID = Random.Range(minValue, maxValue).ToString();

            _createdRoomView.TextRandomRoomId.text = $"ID Комнаты: {randomID}";

            _startMenuView.gameObject.SetActive(false);
            _createdRoomView.gameObject.SetActive(true);

            _fusionConnector.StartFusionSession(GameMode.Host, randomID);
        }

        private void OnJoinButtonClicked()
        {
            int countNumberId = 4;

            string enterIdRoom = _startMenuView.InputText.text;

            if (string.IsNullOrEmpty(enterIdRoom))
            {
                Debug.Log("Вы не ввели ID комнаты.");
                return;
            }

            if (enterIdRoom.Length != countNumberId)
            {
                Debug.Log("Код ID должен состояить из 4 цифр.");
                return;
            }

            _startMenuView.gameObject.SetActive(false);

            _fusionConnector.StartFusionSession(GameMode.Client, enterIdRoom);
        }
    }
}