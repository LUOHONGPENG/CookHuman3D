using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CookwareBasic
{
    private void InvokeMarriage(HumanBasic human)
    {
        human.yearMarriage = 1f;
    }


    private void InvokeRetire(HumanBasic human)
    {
        human.isRetired = true;
    }


}
