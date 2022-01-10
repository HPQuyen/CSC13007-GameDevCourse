using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameDevCourse.Hero
{
    public class HeroPharmacist : Hero
    {
        #region Override Callbacks

        // ===========  Attack  ===========
        protected override void Hit(Virus enemy)
        {
            mAnimator.SetTrigger("OnAttack");

            if (isHitable)
            {
                enemy.OnTakeDamage(heroStats.meleeAttackDamage);

                isHitable = false;
            }

            meleeAttackCooldownCounter += Time.deltaTime;

            if (meleeAttackCooldownCounter >= heroStats.meleeFireRate)
            {
                meleeAttackCooldownCounter = 0f;

                isHitable = true;
            }
        }

        protected override void Shoot(Virus enemy)
        {
            mAnimator.SetTrigger("OnAttack");

            if (isShootable)
            {
                Instantiate(projectileType).Launch(gameObject, enemy.gameObject);

                isShootable = false;
            }

            rangedAttackCooldownCounter += Time.deltaTime;

            if (rangedAttackCooldownCounter >= heroStats.rangeFireRate)
            {
                rangedAttackCooldownCounter = 0f;

                isShootable = true;
            }
        }

        // ===========  Cast Skills  ===========
        
            // TODO: Implement Hero's unique skill casting
        
        // ===========  Take Damage  ===========
        
            // TODO: Implement Hero's taking damage event

        // ===========  Die  ===========
        
            // TODO: Implement Hero's dying event

        // ===========  Revive  ===========
            
            // TODO: Implement Hero's resurrection event

        #endregion
    }
}
