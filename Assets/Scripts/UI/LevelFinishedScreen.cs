using LudumDare.Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LudumDare.Assets.Scripts.UI {
    class LevelFinishedScreen : MonoBehaviour{
        [SerializeField]
        GameObject root;

        [SerializeField]
        Button continueButton;
        [SerializeField]
        Button mainMenuButton;
        [SerializeField]
        Button quitButton;

        protected void OnEnable() {
            EnemySpawner.onAllEnemiesKilled += EnableScreen;
            continueButton.onClick.AddListener(Continue);
            mainMenuButton.onClick.AddListener(MainMenu);
            quitButton.onClick.AddListener(Quit);
            DisableScreen();
        }

        protected void OnDisable() {
            EnemySpawner.onAllEnemiesKilled -= EnableScreen;
        }

        void EnableScreen() {
            root.SetActive(true);
        }

        void DisableScreen() {
            root.SetActive(false);
        }

        void Continue() {
            Debug.Log("Continue");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void MainMenu() {
            Debug.Log("Main Menu");
        }

        void Quit() {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
