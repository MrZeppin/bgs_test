using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BGSTest
{
    public static class PauseManager
    {
        private static bool isPaused;
        public static bool IsPaused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                Time.timeScale = isPaused ? 0f : 1f;
            }
        }
        #if UNITY_EDITOR
        // for when "Reload Domain" is set to false in ProjectSettings
        [InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            IsPaused = false;
        }
        #endif
    }
}