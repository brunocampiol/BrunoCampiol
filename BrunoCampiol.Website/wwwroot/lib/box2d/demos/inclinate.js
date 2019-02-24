// World NOT being used right now
// Needs to be added in the bundleconfig to load

demos.inclinate = {};
demos.inclinate.initWorld = function (world) {

    var ground = world.m_groundBody;

    createBox(world, 0, 1, 800, 10);

    // INCLINATED RECTANGLE
    var boxSd = new b2BoxDef();
    //boxSd.density = 1.0;          // No density means fixed object
    boxSd.extents.Set(300, 2);
    var boxBd = new b2BodyDef();
    boxBd.AddShape(boxSd);
    boxBd.position.Set(240, 300);
    boxSd.localRotation = 0.7;
    world.CreateBody(boxBd)

    // MOTOR DEFINITION HERE
    // The object motor size and density
    var boxObject = new b2BoxDef();
    boxObject.extents.Set(5, 80);
    boxObject.density = 1.0;

    var bodyDef = new b2BodyDef();
    bodyDef.AddShape(boxObject);
    bodyDef.position.Set(705, 408);
    var worldBody = world.CreateBody(bodyDef);

    // Creates the spinning object
    var pistonRotation = new b2RevoluteJointDef();
    pistonRotation.anchorPoint.Set(705, 408);
    pistonRotation.body1 = world.m_groundBody;
    pistonRotation.body2 = worldBody;
    pistonRotation.motorSpeed = 4.0 * Math.PI;
    pistonRotation.motorTorque = 9000000000000.0;
    pistonRotation.enableMotor = true;
    world.CreateJoint(pistonRotation);


}
demos.InitWorlds.push(demos.inclinate.initWorld);
