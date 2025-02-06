using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Ссылка на ползунок
    public AudioSource audioSource; // Ссылка на AudioSource, который управляет звуком

    private const string VolumeKey = "GameVolume"; // Ключ для сохранения уровня громкости

    void Start()
    {
        // Загружаем сохраненный уровень громкости, если он есть
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
            audioSource.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            // Если сохраненного значения нет, устанавливаем громкость на 50%
            audioSource.volume = 0.5f;
            volumeSlider.value = 0.5f;
        }

        // Добавляем обработчик изменения значения ползунка
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float volume)
    {
        // Устанавливаем громкость в AudioSource
        audioSource.volume = volume;

        // Сохраняем уровень громкости
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}