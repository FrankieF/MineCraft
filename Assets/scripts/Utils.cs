﻿using UnityEngine;

public class Utils {

    static int maxHeight = 150;
    static float smooth = 0.01f;
    static int octaves = 4;
    static float persistence = 0.5f;

    public static int GenerateHeight(float x, float z)
    {
        float height = Map(0, maxHeight, 0, 1, FBM(x * smooth, z * smooth, octaves, persistence));
        return (int)height;
    }

    public static int GenerateStoneHeight(float x, float z)
    {
        float height = Map(0, maxHeight - 5, 0, 1, FBM(x * smooth * 2, z * smooth * 2, octaves + 1, persistence));
        return (int)height;
    }

    public static int GenerateRedStoneHeight(float x, float z)
    {
        float height = Map(0, maxHeight - 130, 0, 1, FBM(x * smooth * 2, z * smooth * 2, octaves - 1, persistence));
        return (int)height;
    }

    static float Map(float newmin, float newmax, float origomin, float origmax, float value)
    {
        return Mathf.Lerp(newmin, newmax, Mathf.InverseLerp(origomin, origmax, value));
    }

    static float FBM(float x, float z, int octaves, float persistence)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        float offset = 32000f;
        for (int i = 0; i < octaves; i++)
        {
            total += Mathf.PerlinNoise((x+offset) * frequency, (z+offset) * frequency) * amplitude;
            maxValue += amplitude;
            amplitude *= persistence;
            frequency *= 2;
        }
        return total / maxValue;
    }

    public static float FBM3D(float x, float y, float z, float sm, int oct)
    {
        float XY = FBM(x * sm, y * sm, oct, 0.5f);
        float YZ = FBM(y * sm, z * sm, oct, 0.5f);
        float XZ = FBM(x * sm, z * sm, oct, 0.5f);
                   
        float YX = FBM(y * sm, x * sm, oct, 0.5f);
        float ZY = FBM(z * sm, y * sm, oct, 0.5f);
        float ZX = FBM(z * sm, x * sm, oct, 0.5f);

        return (XY + YZ + XZ + YX + ZY + ZX) / 6.0f;
    }

}