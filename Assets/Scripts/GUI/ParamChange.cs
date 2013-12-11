using UnityEngine;
using System.Collections;

public class ParamChange {
    public float time;
    public ParamType paramType;
    public string entity;
    public float value;

    public ParamChange() {

    }

    public ParamChange(float time, ParamType paramType, string entity, float value) {
        this.time = time;
        this.paramType = paramType;
        this.entity = entity;
        this.value = value;
    }

    public override string ToString() {
        string str = "(time, parameter, entity, value) (" + time + ", " + paramType + ", " + entity + ", " + value + ")";
        return str;
    }
}