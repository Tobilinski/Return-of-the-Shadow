using UnityEngine;

[CreateAssetMenu(fileName = "VariableReference", menuName = "VariableReference/Int Variable")]
public class IntVariableReference : VariableReference<int>
{
    public void SetValue(int value)
    {
        this.value = value;
    }
}
