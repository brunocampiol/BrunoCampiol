// Draws the pendulums
// This is the 4th world

demos.pendulum = {};
demos.pendulum.initWorld = function (world) {
    var i;
    var ground = world.GetGroundBody();
    var jointDef = new b2RevoluteJointDef();
    var L = 150;
    for (i = 0; i < 4; i++) {
        jointDef.anchorPoint.Set(250 + 40 * i, 200 - L);
        jointDef.body1 = ground;
        jointDef.body2 = createBall(world, 250 + 40 * i, 200, 20, 1, 0.1);
        world.CreateJoint(jointDef);
    }
    jointDef.anchorPoint.Set(250 - 40, 200 - L);
    jointDef.body1 = ground;
    jointDef.body2 = createBall(world, 250 - 40 - L, 200 - L, 20, 1, 0.1);
    world.CreateJoint(jointDef);
}
demos.InitWorlds.push(demos.pendulum.initWorld);