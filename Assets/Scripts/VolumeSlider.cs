using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private AudioSource Audiosource;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(val => AudioManager.Instance.ChangeVolumeWithSlider(Audiosource, val));
    }
}
