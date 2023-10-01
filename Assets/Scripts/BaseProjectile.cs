using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LudumDare.Assets.Scripts {
    public abstract class BaseProjectile : MonoBehaviour, IProjectile {
        public abstract void Shoot(Vector2 direction);
    }
}
