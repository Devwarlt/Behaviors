/*Been working on the tomb bosses, thought I may as well make a turorial on how I did it.
 * Please scroll down to Geb, I'll add comments on him
 */




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.realm;
using wServer.logic.attack;
using wServer.logic.movement;
using wServer.logic.loot;
using wServer.logic.taunt;
using wServer.logic.cond;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        static _ TombDefender = Behav()
            .Init(0x0d24, Behaves("Pyramid Artifact 3",
           IfNot.Instance(
                        Circling.Instance(2, 3, 10, 0x0d27),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d23, Behaves("Pyramid Artifact 2",
                    IfNot.Instance(
                        Circling.Instance(2, 3, 10, 0x0d26),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d22, Behaves("Pyramid Artifact 1",
                    IfNot.Instance(
                        Circling.Instance(2, 3, 10, 0x0d28),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d1b, Behaves("Scarab",
                    
                    new RunBehaviors(
                        Chasing.Instance(13, 25, 0, null),
                   Cooldown.Instance(500, SimpleAttack.Instance(10, projectileIndex: 0)),
                    Cooldown.Instance(500, SimpleAttack.Instance(10, projectileIndex: 1))
                )))
                .Init(0x0d1e, Behaves("Sphinx Artifact 3",
                    IfNot.Instance(
                        Chasing.Instance(10, 25, 0, null),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d1d, Behaves("Sphinx Artifact 2",
                    IfNot.Instance(
                        Circling.Instance(2, 3, 15, 0x0d26),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d1c, Behaves("Sphinx Artifact 1",
                    IfNot.Instance(
                        Circling.Instance(2, 3, 15, 0x0d26),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d21, Behaves("Nile Artifact 3",
                    IfNot.Instance(
                        Chasing.Instance(10, 25, 0, null),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d20, Behaves("Nile Artifact 2",
                    IfNot.Instance(
                        Circling.Instance(2, 3, 10, 0x0d27),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
                .Init(0x0d1f, Behaves("Nile Artifact 1",
                    IfNot.Instance(
                        Circling.Instance(2, 3, 10, 0x0d27),
                        SimpleWandering.Instance(20)
                    ),
                    Cooldown.Instance(500, SimpleAttack.Instance(10))
                ))
            .Init(0x0d28, Behaves("Tomb Defender",
           
           new RunBehaviors(
                        HpGreaterEqual.Instance(10000,
                            new RunBehaviors(
                                Circling.Instance(5, 10, 5, 0x0d25),
                                True.Instance(Once.Instance(new SimpleTaunt("THIS WILL NOW BE YOUR TOMB!")))
                            )
                        ),
                        HpLesserPercent.Instance(0.99f,
                            new RunBehaviors(
                             Once.Instance(RingAttack.Instance(25, 10, projectileIndex: 3))
                            )

                    ),
                    //HpLesserPercent.Instance(0.98f,
            //HpGreaterEqual.Instance(98000,
                   // new RunBehaviors(
                      //  Once.Instance(SetConditionEffect.Instance(ConditionEffectIndex.Armored)),
                     //   Cooldown.Instance(5000, RingAttack.Instance(4, 10, projectileIndex: 1)),
                      //  Cooldown.Instance(5000, RingAttack.Instance(5, 10, projectileIndex: 0)),
                      //  Cooldown.Instance(2000, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 2))
                   // ))),
                        HpLesserPercent.Instance(0.90f,
                    new RunBehaviors(
                        Cooldown.Instance(5000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 5, 0, projectileIndex: 1)),
                        Cooldown.Instance(5000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 3, 0, projectileIndex: 0)),
                        Cooldown.Instance(2000, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 8, 0, projectileIndex: 2))
                    )),
                       HpLesserPercent.Instance(0.1f,
                            new RunBehaviors(
                                Flashing.Instance(500, 0xffff3333),
                                Chasing.Instance(6, 6, 0, null),
                        Cooldown.Instance(5000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 5, 0, projectileIndex: 1)),
                        Cooldown.Instance(5000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 3, 0, projectileIndex: 0)),
                        Cooldown.Instance(500, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 5, 0, projectileIndex: 4)),
                                Once.Instance(new SimpleTaunt("The end of your path is here!"))
                            )

                    ),
                    HpLesserPercent.Instance(0.5f,
                        Once.Instance(
                            new RunBehaviors(
                                True.Instance(Once.Instance(new SimpleTaunt("My artifacts shall prove my wall of defense is impenetrable!"))),
                                SpawnMinionImmediate.Instance(0x0d22, 1, 1, 4),
                                SpawnMinionImmediate.Instance(0x0d23, 1, 1, 4),
                                SpawnMinionImmediate.Instance(0x0d24, 1, 1, 4)
                    )))),

                    
                        loot: new LootBehavior(LootDef.Empty,
                        Tuple.Create(100, new LootDef(0, 2, 0, 2,
                            Tuple.Create(0.5, (ILoot)new StatPotionLoot(StatPotion.Life)),
                            Tuple.Create(0.15, (ILoot)new ItemLoot("Tome of Holy Protection")),
                            Tuple.Create(0.25, (ILoot)new ItemLoot("Ring of the Pyramid"))
                            )

           )))); 

        static _ TombSupport = Behav()
            .Init(0x0d26, Behaves("Tomb Support",
             new RunBehaviors(
                        HpGreaterEqual.Instance(5000,
                            new RunBehaviors(
                                Circling.Instance(5, 10, 7, 0x0d25),
                                Cooldown.Instance(1000, Heal.Instance(6f, 100, 0x0d28)),
                                True.Instance(Once.Instance(new SimpleTaunt("YOU HAVE AWAKENED US!")))
                            )
                        ),
                        HpLesserPercent.Instance(0.99f,
                            new RunBehaviors(
                             Once.Instance(RingAttack.Instance(25, 10, projectileIndex: 7))
                            )

                    ),
                        HpLesserPercent.Instance(0.98f,
                    new RunBehaviors(
                        
                        Cooldown.Instance(2000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 1, 0, projectileIndex: 5)),
                        Cooldown.Instance(5000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 1, 0, projectileIndex: 6)),
                        Cooldown.Instance(3000, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 1)),
                        Cooldown.Instance(3500, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 2)),
                        Cooldown.Instance(3500, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 3)),
                        Cooldown.Instance(3500, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 4)),
                        Cooldown.Instance(5000, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 7, 0, projectileIndex: 0))
                    )),
                    HpLesserPercent.Instance(0.0625f,
                            new RunBehaviors(
                                Flashing.Instance(500, 0xffff3333),
                                Chasing.Instance(8, 10, 0, null),
                                Cooldown.Instance(3000, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 1)),
                        Cooldown.Instance(3500, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 2)),
                        Cooldown.Instance(3500, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 3)),
                                Cooldown.Instance(500, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 5, 0, projectileIndex: 8)),
                                Cooldown.Instance(700, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 1, 0, projectileIndex: 5)),
                                Cooldown.Instance(4000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 2, 0, projectileIndex: 6)),
                                Once.Instance(new SimpleTaunt("This cannot be! You shall not succeed!"))
                            )
                        
                    ),

     
                    HpLesserPercent.Instance(0.5f,
                        Once.Instance(
                            new RunBehaviors(
                                True.Instance(Once.Instance(new SimpleTaunt("My artifacts shall make your lethargic lives end much more switfly!"))),
                                SpawnMinionImmediate.Instance(0x0d1c, 1, 1, 4),
                                SpawnMinionImmediate.Instance(0x0d1d, 1, 1, 4),
                                SpawnMinionImmediate.Instance(0x0d1e, 1, 1, 4),

                                EntityGroupLesserThan.Instance(15, 5, "Sphinx Artifacts")
                    )))),
                    

                        loot: new LootBehavior(LootDef.Empty,
                        Tuple.Create(100, new LootDef(0, 2, 0, 2,
                            Tuple.Create(0.5, (ILoot)new StatPotionLoot(StatPotion.Life)),
                            Tuple.Create(0.15, (ILoot)new ItemLoot("Wine Cellar Incantation")),
                            Tuple.Create(0.5, (ILoot)new ItemLoot("Ring of the Sphinx"))
                            )

            ))));

        static _ TombAttacker = Behav()
            .Init(0x0d27, Behaves("Tomb Attacker",
            new RunBehaviors(
                        HpGreaterEqual.Instance(5000, //If his HP ig greater than 5000 (rage), 
                            new RunBehaviors(
                                Circling.Instance(5, 10, 6, 0x0d25), //then circle the Tomb Anchor
                                True.Instance(Once.Instance(new SimpleTaunt("ENOUGH OF YOUR VANDALISM")))  // Initial Taunt

                            )
                        ),
                        HpLesserPercent.Instance(0.99f,
                            new RunBehaviors(
                             Once.Instance(RingAttack.Instance(25, 10, projectileIndex: 3))
                            )

                    ),
                            
                        HpLesserPercent.Instance(0.98f, 
                    new RunBehaviors(
                        
                        Cooldown.Instance(700/* cooldown (fire rate)*/, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 2,/* 2 shots*/ 0, projectileIndex: 2)),   // Multi attack, Multiple projectiles, (same projectile), 
                        Cooldown.Instance(3000, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 1)), /* Green Slow Rings*/
                        Cooldown.Instance(3100, ThrowAttack.Instance(4, 8, 120)),/* Bomb 1*/
                        Cooldown.Instance(2000, ThrowAttack.Instance(3, 8, 70)),/* Bomb 2*/
                        Cooldown.Instance(2500, ThrowAttack.Instance(10, 12, 40))/* Anti Spectate Bomb*/
                    )),
                    HpLesserPercent.Instance(0.055f, /*If Hp is less than 5000, activate behaviors (rage)*/
                            new RunBehaviors(
                                Flashing.Instance(500, 0xffff3333), /* Flash red when rage*/
                                MaintainDist.Instance(50, 3, 9, null), /* This creates Geb's Skipping, he is maintaing distance from players*/
                                True.Instance(Once.Instance(SpawnMinionImmediate.Instance(0x0d1f, 1, 1, 4))),   //Artifact 1 (Circles Geb)
                                True.Instance(Once.Instance(SpawnMinionImmediate.Instance(0x0d20, 1, 1, 4))),   //Artifact 2 (Circles Bes and Nut)
                                True.Instance(Once.Instance(SpawnMinionImmediate.Instance(0x0d21, 1, 1, 4))),    //Artifact 3 (chases)
                                Cooldown.Instance(1000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 2, 0, projectileIndex: 2)),
                                Cooldown.Instance(4000, RingAttack.Instance(5, 0, 5, projectileIndex: 0)),/*Just some random Black shots during rage.*/
                                Cooldown.Instance(500, AngleAttack.Instance(225)),/*Just some random Black shots during rage.*/
                                Cooldown.Instance(500, AngleAttack.Instance(36)),/*Just some random Black shots during rage.*/
                                Cooldown.Instance(500, AngleAttack.Instance(0)),/*Just some random Black shots during rage.*/
                                Cooldown.Instance(500, AngleAttack.Instance(135)),/*Just some random Black shots during rage.*/
                                Cooldown.Instance(500, AngleAttack.Instance(90)),/*Just some random Black shots during rage.*/
                                Cooldown.Instance(1000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 2, 0, projectileIndex: 5)), /*Fire Magic Shots.*/
                                Cooldown.Instance(3000, MultiAttack.Instance(25, 45 * (float)Math.PI / 180, 10, 0, projectileIndex: 1)),/*Green Slow Boomerang*/
                                Once.Instance(new SimpleTaunt("Argh! You shall pay for your crimes!")) /*Taunt for Rage*/
                            )

                    ),


                    HpLesserPercent.Instance(0.25f, /*If HP is less than 25%, run new behaviors*/
                        Once.Instance(/*Run theese commands ONCE, to prevent infinite Spawning.*/
                            new RunBehaviors(
                                True.Instance(Once.Instance(new SimpleTaunt("My artifacts shall destroy you from your soul to your flesh!"))), /*Artifact Taunt.*/
                                SpawnMinionImmediate.Instance(0x0d1f, 1, 1, 4),   //Spawn Artifact 1
                                SpawnMinionImmediate.Instance(0x0d20, 1, 1, 4),   //Spawn Artifact 2
                                SpawnMinionImmediate.Instance(0x0d21, 1, 1, 4)    //Spawn Artifact 3
                    )))),


                        loot: new LootBehavior(LootDef.Empty, /*Class for loot.*/
                        Tuple.Create(100, new LootDef(0, 2, 0, 2,
                            Tuple.Create(0.5, (ILoot)new StatPotionLoot(StatPotion.Life)),
                            Tuple.Create(0.15, (ILoot)new ItemLoot("Wine Cellar Incantation")),
                            Tuple.Create(0.5, (ILoot)new ItemLoot("Ring of the Nile"))
                            )

            ))));

        static _ LionArcher = Behav()
         .Init(0x0d3c, Behaves("Lion Archer",
           IfNot.Instance(
                        Chasing.Instance(11, 8, 6, 0x617),
                        IfNot.Instance(
                            Chasing.Instance(8, 8, 0, null),
                            SimpleWandering.Instance(4)
                        )
                    ),
          new RunBehaviors(
          Cooldown.Instance(1000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 1, 0, projectileIndex: 0)),
          Cooldown.Instance(1200, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 1, 0, projectileIndex: 1)),
          Cooldown.Instance(1300, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 1, 0, projectileIndex: 2)),
          Cooldown.Instance(1000, Once.Instance(RingAttack.Instance(5, 0, 5, projectileIndex: 3)))
          ),
          loot: new LootBehavior(
                        new LootDef(0, 2, 0, 8,
                            Tuple.Create(0.03, (ILoot)HpPotionLoot.Instance),
                           Tuple.Create(0.03, (ILoot)MpPotionLoot.Instance)
                        )
                    )))

                    .Init(0x0d19, Behaves("Jackal Lord",
                    new RunBehaviors(
                        Chasing.Instance(7, 10, 0, null),
                        Cooldown.Instance(1000, MultiAttack.Instance(25, 10 * (float)Math.PI / 180, 3, 0, projectileIndex: 0))
                    )

            ));
        
    }
}