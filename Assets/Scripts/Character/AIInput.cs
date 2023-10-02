using LudumDare.Assets.Scripts;
using UnityEngine;

namespace LudumDare {
    class AIInput : MonoBehaviour {
        [SerializeField]
        float attackRange = 0.5f;
        [SerializeField]
        bool useMelee = true;
        [SerializeField]
        bool useRange;
        GameObject playerObject;

        bool disabled;

        protected void OnEnable() {
            Player.onDeath += Disable;    
        }

        protected void OnDisable() {
            Player.onDeath -= Disable;
        }

        protected void Start() {
            playerObject = Player.instance.gameObject;
        }

        protected void FixedUpdate() {
            if (!playerObject || !playerObject.activeInHierarchy || disabled) {
                gameObject.SendMessage("OnMove", Vector2.zero);
                return;
            }

            if (Vector2.Distance(transform.position, playerObject.transform.position) > attackRange) {
                Chase();
            } else {
                Attack();
            }
        }

        void Attack() {
            Vector2 dir = playerObject.transform.position - transform.position;
            gameObject.SendMessage("OnMove", dir.normalized);
            if (useMelee) {
                gameObject.SendMessage("OnAttack");
            }
            if (useRange) {
                gameObject.SendMessage("OnRangedAttack");
            }
        }

        void Chase() {
            Vector2 dir = playerObject.transform.position - transform.position;
            gameObject.SendMessage("OnMove", dir.normalized);
        }

        void Disable() {
            disabled = true;
        }
    }
}
