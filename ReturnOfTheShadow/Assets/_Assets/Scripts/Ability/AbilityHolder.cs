using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "NewAbilityHolder", menuName = "Abilities/Ability Holder")]
public class AbilityHolder : ScriptableObject
{
    [SerializeReference]public List<Ability> abilities = new List<Ability>();

}


