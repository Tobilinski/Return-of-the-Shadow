using UnityEngine;
public abstract class VariableReference<T> : ScriptableObject
{
    public T value;
    public static implicit operator T(VariableReference<T> reference)
    {
        return reference.value;
    }
}