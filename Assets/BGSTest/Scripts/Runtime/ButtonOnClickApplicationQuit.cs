using UnityEngine;
using UnityEngine.UI;

namespace BGSTest
{
    public class ButtonOnClickApplicationQuit : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Application.Quit);
        }
    }
}