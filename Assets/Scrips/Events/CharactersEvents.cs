﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;

  public class CharactersEvents
    {
    public static UnityAction<GameObject, int> characterDamaged;

    public static UnityAction<GameObject, int> characterHealed ;

    public static UnityAction<GameObject, int> characterMana;

}

