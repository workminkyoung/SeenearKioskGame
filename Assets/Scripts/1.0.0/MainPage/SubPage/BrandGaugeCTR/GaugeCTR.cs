using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GaugeCTR : MonoBehaviour
{
    public eContent content;
    private List<Button> startBtn = new List<Button>();
    private List<Transform> stamps = new List<Transform>();

    public void Setting()
    {
        startBtn.AddRange(GetComponentsInChildren<Button>());
        for (int i = 0; i < startBtn.Count; i++)
        {
            startBtn[i].gameObject.SetActive(false);
            startBtn[i].onClick.AddListener(ChangeScene);
        }
        stamps.AddRange(UtilityExtensions.GetComponentsOnlyInChildrenByTag_NonRecursive<Transform>(transform, "stamp"));
        for (int i = 0; i < stamps.Count; i++)
        {
            stamps[i].gameObject.SetActive(false);
        }
    }

    public void SetContentState(eTrainingType trainingType)
    {
        // activate start btn match with training type
        for (int i = 0; i < startBtn.Count; i++)
        {
            startBtn[i].gameObject.SetActive(false);
        }
        startBtn[(int)trainingType].gameObject.SetActive(true);

        // enable stamp match with userdata progress data
        string key = content.ToString() + trainingType.ToString();
        StampUpdate(System.Convert.ToBoolean(UserDataManager.userData.contentProgress[key]));
    }

    public void StampUpdate(bool state)
    {
        stamps[(int)eStamp.off].gameObject.SetActive(!state);
        stamps[(int)eStamp.on].gameObject.SetActive(state);
    }

    void ChangeScene()
    {
        string sceneName = content.ToString() + PageManager.instance.curTraingingType.ToString();
        SceneManager.LoadScene(sceneName);
    }

    enum eStamp
    {
        off = 0,
        on
    }
}
