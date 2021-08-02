using UnityEngine;
using Between.SpellRecognition;
using Between.SpellsEffects.Projectile;
using Between.InputTracking.Trackers;
using Between.InputTracking;
using System.Collections.Generic;

namespace Between.Spells
{
    public class ProjectileSpell : SvmBasedSpell
    {
        protected override SpellFigure _figure => SpellFigure.Line;
        protected override float _manaCoefficient => _manaCoeff;

        private readonly ProjectileSpawner _projectileSpawner;

        private float _minLenght => GameSettings.Instance.ProjectileMinLenght;
        private float _maxLenght => GameSettings.Instance.ProjectileMaxLenght;
        private float _manaCoeff => GameSettings.Instance.ProjectileManaCoefficient;
        private float _powerValue => GameSettings.Instance.ProjectilePowerValue;
        private float _baseDamage => GameSettings.Instance.ProjectileBaseDamageValue;

        private bool _isValidLenght
        {
            get
            {
                var points = ((SvmTracker)tracker).DrawPoints;

                if (points.Count < 2)
                    return false;

                var distance = Vector2.Distance(points[0], points[points.Count - 1]);
                return distance > _minLenght && distance < _maxLenght;
            }
        }

        public ProjectileSpell(string projectileName) : base()
        {
            _projectileSpawner = new ProjectileSpawner(projectileName);
        }

        protected override void OnCompleteSpell()
        {
            if (_isValidLenght && _enoughMana)
            {
                SpawnProjectile();
                PerformOnCastSpell();
            }
        }

        private void SpawnProjectile()
        {
            List<Vector2Int> points = ((SvmTracker)tracker).DrawPoints;
            Vector3 startPoint = ConvertVector(points[0]);
            Vector3 directionPoint = ConvertVector(points[points.Count - 1]);

            if (GameSettings.Instance.ProjectileDrawType == ProjectileDrawType.Slingshot)
            {
                Vector3 tempPoint = startPoint;
                startPoint = directionPoint;
                directionPoint = tempPoint;
            }

            UpdateProjectileDamage();
            _projectileSpawner.Spawn(startPoint, (directionPoint - startPoint).normalized);
        }

        private Vector3 ConvertVector(Vector2Int input) 
            => GameCamera.ScreenToWorldPoint(input);

        private void UpdateProjectileDamage()
        {
            float relativeLenght = InputLenghtCalculator.LastLenght / _minLenght;
            float damageValue = Mathf.Pow(relativeLenght, _powerValue);

            _projectileSpawner.ChangeDamageValue(damageValue);
        }

        public enum ProjectileDrawType
        {
            Spell = 0,
            Slingshot
        }
    }
}