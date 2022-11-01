﻿using UnityEngine;

namespace CodeBase.Extensions
{
    public static class GameObjectExtension
    {
        public static void Activate(this GameObject gameObject) =>
            gameObject.SetActive(true);
        
        public static void Deactivate(this GameObject gameObject) =>
            gameObject.SetActive(false);
    }
}