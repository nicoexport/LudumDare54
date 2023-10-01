using System;
using LudumDare.Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LudumDare {
    class AIInput : MonoBehaviour {
        [SerializeField]
        float attackRange = 0.5f;
        GameObject playerObject;

        protected void Start() {
            playerObject = Player.instance.gameObject;
        }

        protected void FixedUpdate() {
            if(Vector2.Distance(transform.position, playerObject.transform.position) > attackRange) {
                Chase();
            } else {
                Attack();
            }
        }

        void Attack() {
        }

        void Chase() {
            Vector2 dir = playerObject.transform.position - transform.position;
            gameObject.SendMessage("OnMove", dir.normalized);
        }
    }
}
