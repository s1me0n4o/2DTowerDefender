using Assets.ProjectFiles.Scripts.Enums;
using UnityEngine.SceneManagement;

namespace Assets.ProjectFiles.Scripts
{
    public static class GameSceneManager
    {
        public static void LoadScene(GameScene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }

}
