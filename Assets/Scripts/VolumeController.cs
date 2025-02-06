using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // ������ �� ��������
    public AudioSource audioSource; // ������ �� AudioSource, ������� ��������� ������

    private const string VolumeKey = "GameVolume"; // ���� ��� ���������� ������ ���������

    void Start()
    {
        // ��������� ����������� ������� ���������, ���� �� ����
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
            audioSource.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            // ���� ������������ �������� ���, ������������� ��������� �� 50%
            audioSource.volume = 0.5f;
            volumeSlider.value = 0.5f;
        }

        // ��������� ���������� ��������� �������� ��������
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float volume)
    {
        // ������������� ��������� � AudioSource
        audioSource.volume = volume;

        // ��������� ������� ���������
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}