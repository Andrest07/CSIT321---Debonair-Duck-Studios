* Duplicate blank prefab, create & add new enemy scriptable object

// create animator & animations
* In Assets/Animations/Enemy, duplicate _Enemy folder and rename everything for new enemy
* In the animator inspector, set:
	EnemyAttack : <Name>Attack
	EnemyIdle : <Name>Idle
* Add the animator to the new enemy

// add sprites and attack 
* Add sprites to animations (easiest way is to open up the prefab and open Window>Animation>Animation window, you should see a drop down with the two animations)
* In the <Name>Attack animation, add a new animation event (the little white rectangle) and set the function to Attack()