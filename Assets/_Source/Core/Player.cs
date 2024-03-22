using System;
using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public List<CardInstance> Cards { get; } = new();

        private void Start()
        {
            CardGame.Instance.AddPlayer(this);
        }
    }
}