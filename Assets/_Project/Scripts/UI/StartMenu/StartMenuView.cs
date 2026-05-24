using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button CreateRoomButton { get; private set; }
        [field: SerializeField] public Button JoinRoomButton { get; private set; }
        [field: SerializeField] public TMP_InputField InputText { get; private set; }
    }
}