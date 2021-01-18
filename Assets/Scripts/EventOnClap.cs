using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EventOnClap : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private int minimumSoundLevel = 15;

    [SerializeField]
    private GameObject[] sendMessageTo = new GameObject[0];

    private AudioSource goAudioSource;
    private float[] clipSampleData = new float[1024];

    private void Update()
    {
        goAudioSource.GetSpectrumData(clipSampleData, 0, FFTWindow.Rectangular);
        float currentAverageVolume = Average(clipSampleData) * 10000;
        if (currentAverageVolume >= minimumSoundLevel)
        {
            foreach (GameObject g in sendMessageTo)
            {
                if (g != null && g.activeSelf)
                {
                    g.SendMessage("Clap");
                }
            }
        }
    }

    private float Average(float[] clipSampleData)
    {
        float total = 0;

        foreach (float f in clipSampleData)
        {
            total += f;
        }

        return total / clipSampleData.Length;
    }

    //Use this for initialization
    private void Start()
    {
        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("Microphone not connected!");
        }
        else //At least one microphone is present
        {
            int minFreq;
            int maxFreq;
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            if (minFreq == 0 && maxFreq == 0)
            {
                maxFreq = 44100;
            }

            goAudioSource = this.GetComponent<AudioSource>();

            goAudioSource.clip = Microphone.Start(Microphone.devices[0], true, 60, maxFreq);
            while (!(Microphone.GetPosition(null) > 0)) { }
            goAudioSource.Play();
        }
    }
}