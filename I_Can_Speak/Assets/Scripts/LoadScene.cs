using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void SceneLoader(int SceneIdx) {
        SceneManager.LoadScene(SceneIdx);
    }


    #region MonoBehaviour CallBacks
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    #endregion
}
