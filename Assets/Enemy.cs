using UnityEngine;

abstract public class Enemy
{
    int health;
    int damage;

}

public class ArcherEnemy : Enemy
{

    public ArcherEnemy() {
        //base.health = 10;
        //base.damage = 5;
    }
}


public class SwordEnemy : Enemy
{
    public SwordEnemy() {
        //base.health = 10;
        //base.damage = 5;
    }

}