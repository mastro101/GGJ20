using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetController
{
    static List<int> assignedController = new List<int>();

    public static void SetControllerToPlayer(PlayerMovement _player)
    {
        if (_player.controllerInput == 0)
        {
            for (int i = 1; i <= 4; i++)
            {
                if (assignedController.Contains(i))
                {
                    continue;
                }

                if (Input.GetButtonDown("A" + i.ToString()))
                {
                    assignedController.Add(i);
                    _player.controllerInput = i;
                    Debug.Log("Add " + i);
                }
            }
        }
    }
}