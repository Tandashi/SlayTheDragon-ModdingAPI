namespace Modding.API {
    public class PlayerAPI {
        public static void EnableSword() => Player.Instance.EnableSword();
        public static void DisableSword() => Player.Instance.DisableSword();
        public static void SetDamage(int damage) => Player.Instance.Stats.CurrentAttackPower = damage;
        public static void SetMoveSpeed(float speed) => Player.Instance.Stats.CurrentMoveSpeed = speed;
        public static void SetCurrentPlayerHealth(int hp) => Player.Instance.Stats.CurrentHealth = hp;
        public static void SetPlayerCanTakeDamage(bool v) => Player.Instance.CanTakeDamage = v;
    }
}
