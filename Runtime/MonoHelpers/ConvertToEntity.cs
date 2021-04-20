using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voody.UniLeo
{
    enum ConvertMode
    {
        ConvertAndInject,
        ConvertAndDestroy
    }
    
    public class ConvertToEntity : MonoBehaviour
    {
        [SerializeField] private ConvertMode convertMode;
    }
}
