using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuPresenter : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _sessionPrefab;
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

            StartFusionSession(GameMode.Host, randomID);
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

            StartFusionSession(GameMode.Client, enterIdRoom);
        }

        private void StartFusionSession(GameMode mode, string roomName)
        {
            if (_sessionPrefab == null)
                return;

            NetworkRunner currentRunner = Instantiate(_sessionPrefab);
            currentRunner.name = "PhotonSession";

            var sceneManager = currentRunner.GetComponent<NetworkSceneManagerDefault>();

            int gameplaySceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            currentRunner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = roomName,
                Scene = SceneRef.FromIndex(gameplaySceneIndex),
                SceneManager = sceneManager
            });

            Debug.Log($"Сеть запущена в режиме: {mode}. Комната: {roomName}");
        }
    }
}