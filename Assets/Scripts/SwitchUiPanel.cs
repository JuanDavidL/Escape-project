using UnityEngine;

public class SwitchUiPanel : MonoBehaviour
{
    public void SwitchToPanel(GameObject panelToSwitch)
    {
        panelToSwitch.SetActive(true);
        gameObject.SetActive(false);
    }

    public void TestInLog()
    {
        Debug.Log("TestLog funtion from SwitchPanel was called in " + gameObject.name);
    }
}
