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



        protected void Start() {
            playerObject = Player.instance.gameObject;
        }

        protected void FixedUpdate() {
            if (!playerObject || !playerObject.activeInHierarchy) {
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
    }
}
