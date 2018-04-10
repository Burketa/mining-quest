using UnityEngine;

public class MinMax
{
    public int intMin, intMax;
    public float floatMin, floatMax;
    public Vector2 vec2Min, vec2Max;
    public Vector3 vec3Min, vec3Max;
    public Quaternion quatMin, quatMax;
    public Transform transfMin, transfMax;

    public MinMax(int intMin, int intMax)
    {
        this.intMin = intMin;
        this.intMax = intMax;
    }
    public MinMax(float floatMin, float floatMax)
    {
        this.floatMin = floatMin;
        this.floatMax = floatMax;
    }

    public MinMax(Vector2 vec2Min, Vector2 vec2Max)
    {
        this.vec2Min = vec2Min;
        this.vec2Max = vec2Max;
    }
    public MinMax(Vector3 vec3Min, Vector3 vec3Max)
     {
        this.vec3Min = vec3Min;
        this.vec3Max = vec3Max;
    }
    public MinMax(Quaternion quatMin, Quaternion quatMax)
     {
        this.quatMin = quatMin;
        this.quatMax = quatMax;
    }
    public MinMax(Transform transfMin, Transform transfMax)
    {
        this.transfMin = transfMin;
        this.transfMax = transfMax;
    }
}
