using UnityEngine;

public class TransformNoise : MonoBehaviour
{
    public PerlinNoise[] PositionNoises;
    public PerlinNoise[] RotationNoises;
    public bool AutoApply;
    public float NoiseAmount = 1f;
    public float noiseMultiplier = 1f;

    public bool UpdateFixedUpdate = true;

    public Vector3 NoisedPosition
    {
        get
        {
            return position * NoiseAmount;
        }
    }

    public Quaternion NoisedRotation
    {
        get
        {
            return Quaternion.Euler(eulerRotation * NoiseAmount);
        }
    }

    Vector3 position;
    Vector3 eulerRotation;

    PerlinNoise.PerlinNoiseData noiseData;

    void Start()
    {
        noiseData.Offsets = new Vector3(
                    UnityEngine.Random.Range(-10000f, 10000f),
                    UnityEngine.Random.Range(-10000f, 10000f),
                    UnityEngine.Random.Range(-10000f, 10000f));
    }

    void Update()
    {
        if (!UpdateFixedUpdate)
        {
            updateNoise();
        }
    }

    void FixedUpdate()
    {
        if (UpdateFixedUpdate)
        {
            updateNoise();
        }
    }

    void updateNoise()
    {
        position = Vector3.zero;
        eulerRotation = Vector3.zero;

        Vector3 time = Vector3.one * Time.time;
        noiseData.Time = time;
        foreach (PerlinNoise n in PositionNoises)
        {
            position += n.GetPositionNoise(noiseData);
        }
        foreach (PerlinNoise n in RotationNoises)
        {
            eulerRotation += n.GetRotationNoise(noiseData);
        }

        if (AutoApply)
        {
            transform.localPosition = position * noiseMultiplier;
            transform.localRotation = Quaternion.Euler(eulerRotation * noiseMultiplier);
        }
    }
}

[System.Serializable]
public class PerlinNoise : BasicNoise<PerlinNoise.PerlinNoiseData>
{
    public Vector3 Amplitude;
    public Vector3 Frequency;

    public override Vector3 GetPositionNoise(PerlinNoiseData data)
    {
        float xPos = 0f;
        float yPos = 0f;
        float zPos = 0f;

        Vector3 timeVal = Vector3.Scale(data.Time, Frequency);
        timeVal += data.Offsets;

        Vector3 noise = new Vector3(
                Mathf.PerlinNoise(timeVal.x, 0f) - 0.5f,
                Mathf.PerlinNoise(timeVal.y, 0f) - 0.5f,
                Mathf.PerlinNoise(timeVal.z, 0f) - 0.5f);

        xPos += noise.x * Amplitude.x;
        yPos += noise.y * Amplitude.y;
        zPos += noise.z * Amplitude.z;

        return new Vector3(xPos, yPos, zPos);
    }

    public override Vector3 GetRotationNoise(PerlinNoiseData data)
    {
        float xPos = 0f;
        float yPos = 0f;
        float zPos = 0f;

        Vector3 timeVal = Vector3.Scale(data.Time, Frequency);
        timeVal += data.Offsets;

        Vector3 noise = new Vector3(
                Mathf.PerlinNoise(timeVal.x, 0f) - 0.5f,
                Mathf.PerlinNoise(timeVal.y, 0f) - 0.5f,
                Mathf.PerlinNoise(timeVal.z, 0f) - 0.5f);

        xPos += noise.x * Amplitude.x;
        yPos += noise.y * Amplitude.y;
        zPos += noise.z * Amplitude.z;

        return new Vector3(xPos, yPos, zPos);
    }

    public struct PerlinNoiseData
    {
        public Vector3 Time;
        public Vector3 Offsets;
    }
}

public abstract class BasicNoise<T>
{
    public abstract Vector3 GetPositionNoise(T data);
    public abstract Vector3 GetRotationNoise(T data);
}