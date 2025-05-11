using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
  public void TakeDamage(float damage);
    public void RestoreHealth(float RestorePoints);
    public void HandleDeath();
}
