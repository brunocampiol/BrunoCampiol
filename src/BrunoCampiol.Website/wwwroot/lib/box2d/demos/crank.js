// This is the 5th world
// Map that have a piston

demos.crank = {};
demos.crank.initWorld = function (world) {

    let positionX = 68;
    let positionY = 415;

    var ground = world.m_groundBody;
    var prevBody = ground;

    // Define crank.
    var boxDef = new b2BoxDef();
    boxDef.extents.Set(5, 25);
    boxDef.density = 1.0;
    // Places crank in world
    var bodyDef = new b2BodyDef();
    bodyDef.AddShape(boxDef);
    bodyDef.position.Set(positionX, positionY);
    var body = world.CreateBody(bodyDef);
    // Makes crank spin in circles
    var revoluteJoinDef = new b2RevoluteJointDef();
    revoluteJoinDef.anchorPoint.Set(positionX, positionY + 25);
    revoluteJoinDef.body1 = prevBody;
    revoluteJoinDef.body2 = body;
    revoluteJoinDef.motorSpeed = -1.0 * Math.PI;
    revoluteJoinDef.motorTorque = 900000000.0;
    revoluteJoinDef.enableMotor = true;
    world.CreateJoint(revoluteJoinDef);
    // Stack things together
    prevBody = body;

    // Define follower.
    boxDef.extents.Set(5, 45);
    bodyDef.position.Set(positionX, positionY - 70);
    body = world.CreateBody(bodyDef);
    // Sets anchor point for rotation
    revoluteJoinDef.anchorPoint.Set(positionX, positionY - 25);
    revoluteJoinDef.body1 = prevBody;
    revoluteJoinDef.body2 = body;
    revoluteJoinDef.enableMotor = false;
    world.CreateJoint(revoluteJoinDef);
    // Stack things together
    prevBody = body;

    // Define piston
    boxDef.extents.Set(50, 10);
    bodyDef.position.Set(positionX, positionY - 115);
    body = world.CreateBody(bodyDef);
    // Sets anchor point for rotation
    revoluteJoinDef.anchorPoint.Set(positionX, positionY - 115);
    revoluteJoinDef.body1 = prevBody;
    revoluteJoinDef.body2 = body;
    world.CreateJoint(revoluteJoinDef);
    // Sets prismatic joint
    var prismaticJointDef = new b2PrismaticJointDef();
    prismaticJointDef.anchorPoint.Set(positionX, positionY - 115);
    prismaticJointDef.body1 = ground;
    prismaticJointDef.body2 = body;
    prismaticJointDef.axis.Set(0.0, 1.0);
    prismaticJointDef.motorSpeed = 0.0; // joint friction
    prismaticJointDef.motorForce = 100000.0;
    prismaticJointDef.enableMotor = true;

    world.CreateJoint(prismaticJointDef);


    // Second piston + motor definition
    let positionX2 = 720;
    let positionY2 = 435;

    let width2 = 25;
    let height2 = 5;

    var ground2 = world.m_groundBody;
    var prevBody2 = ground2;

    // Define crank.
    var boxDef2 = new b2BoxDef();
    boxDef2.extents.Set(width2, height2);
    boxDef2.density = 1.0;
    // Places crank in world
    var bodyDef2 = new b2BodyDef();
    bodyDef2.AddShape(boxDef2);
    bodyDef2.position.Set(positionX2, positionY2);
    var body2 = world.CreateBody(bodyDef2);
    // Makes crank spin in circles
    var revoluteJoinDef2 = new b2RevoluteJointDef();
    revoluteJoinDef2.anchorPoint.Set(positionX2 + width2, positionY2);
    revoluteJoinDef2.body1 = prevBody2;
    revoluteJoinDef2.body2 = body2;
    revoluteJoinDef2.motorSpeed = -1.0 * Math.PI;
    revoluteJoinDef2.motorTorque = 900000000.0;
    revoluteJoinDef2.enableMotor = false;
    world.CreateJoint(revoluteJoinDef2);
    // Stack things together
    prevBody2 = body2;

    // Define follower.
    boxDef2.extents.Set(width2 + 20, height2);
    bodyDef2.position.Set(positionX2 - 70, positionY2);
    body2 = world.CreateBody(bodyDef2);
    // Sets anchor point for rotation
    revoluteJoinDef2.anchorPoint.Set(positionX2 - 25, positionY2);
    revoluteJoinDef2.body1 = prevBody2;
    revoluteJoinDef2.body2 = body2;
    revoluteJoinDef2.enableMotor = true;
    world.CreateJoint(revoluteJoinDef2);
    // Stack things together
    prevBody2 = body2;

    // Define piston
    boxDef2.extents.Set(10, 50);
    bodyDef2.position.Set(positionX2 - 115, positionY2);
    body2 = world.CreateBody(bodyDef2);
    // Sets anchor point for rotation
    revoluteJoinDef2.anchorPoint.Set(positionX2 - 115, positionY2);
    revoluteJoinDef2.body1 = prevBody2;
    revoluteJoinDef2.body2 = body2;
    world.CreateJoint(revoluteJoinDef2);
    // Sets prismatic joint
    var prismaticJointDef2 = new b2PrismaticJointDef();
    prismaticJointDef2.anchorPoint.Set(positionX2 - 115, positionY2);
    prismaticJointDef2.body1 = ground;
    prismaticJointDef2.body2 = body2;
    prismaticJointDef2.axis.Set(-1.0, 0.0);
    prismaticJointDef2.motorSpeed = 0.0; // joint friction
    prismaticJointDef2.motorForce = 100000.0;
    prismaticJointDef2.enableMotor = true;

    world.CreateJoint(prismaticJointDef2);

    // Third piston + motor definition
    let positionX3 = 200;
    let positionY3 = 435;

    var ground3 = world.m_groundBody;
    var prevBody3 = ground3;

    // Define crank.
    var boxDef3 = new b2BoxDef();
    boxDef3.extents.Set(width2, height2);
    boxDef3.density = 1.0;
    // Places crank in world
    var bodyDef3 = new b2BodyDef();
    bodyDef3.AddShape(boxDef3);
    bodyDef3.position.Set(positionX3, positionY3);
    var body3 = world.CreateBody(bodyDef3);
    // Makes crank spin in circles
    var revoluteJoinDef3 = new b2RevoluteJointDef();
    revoluteJoinDef3.anchorPoint.Set(positionX3 - width2, positionY3);
    revoluteJoinDef3.body1 = prevBody3;
    revoluteJoinDef3.body2 = body3;
    revoluteJoinDef3.motorSpeed = -1.0 * Math.PI;
    revoluteJoinDef3.motorTorque = 900000000.0;
    revoluteJoinDef3.enableMotor = false;
    world.CreateJoint(revoluteJoinDef3);
    // Stack things together
    prevBody3 = body3;

    // Define follower.
    boxDef3.extents.Set(width2 + 20, height2);
    bodyDef3.position.Set(positionX3 + 70, positionY3);
    body3 = world.CreateBody(bodyDef3);
    // Sets anchor point for rotation
    revoluteJoinDef3.anchorPoint.Set(positionX3 + 25, positionY3);
    revoluteJoinDef3.body1 = prevBody3;
    revoluteJoinDef3.body2 = body3;
    revoluteJoinDef3.enableMotor = true;
    world.CreateJoint(revoluteJoinDef3);
    // Stack things together
    prevBody3 = body3;

    // Define piston
    boxDef3.extents.Set(10, 50);
    bodyDef3.position.Set(positionX3 + 115, positionY3);
    body3 = world.CreateBody(bodyDef3);
    // Sets anchor point for rotation
    revoluteJoinDef3.anchorPoint.Set(positionX3 + 115, positionY3);
    revoluteJoinDef3.body1 = prevBody3;
    revoluteJoinDef3.body2 = body3;
    world.CreateJoint(revoluteJoinDef3);
    // Sets prismatic joint
    var prismaticJointDef3 = new b2PrismaticJointDef();
    prismaticJointDef3.anchorPoint.Set(positionX3 + 115, positionY3);
    prismaticJointDef3.body1 = ground;
    prismaticJointDef3.body2 = body3;
    prismaticJointDef3.axis.Set(1.0, 0.0);
    prismaticJointDef3.motorSpeed = 0.0; // joint friction
    prismaticJointDef3.motorForce = 100000.0;
    prismaticJointDef3.enableMotor = true;

    world.CreateJoint(prismaticJointDef3);


    // Capsules for cranks
    var boxObject = new b2BoxDef();
    boxObject.extents.Set(95, 2);
    boxObject.density = 0.0;

    let capsuleX = 692;
    let capsuleY = 380;

    var boxBodyDef = new b2BodyDef();
    boxBodyDef.AddShape(boxObject);
    boxBodyDef.position.Set(capsuleX, capsuleY);
    world.CreateBody(boxBodyDef);

    boxObject = new b2BoxDef();
    boxObject.extents.Set(95, 2);
    boxObject.density = 0.0;

    let capsuleX2 = 232;
    let capsuleY2 = 380;

    boxBodyDef = new b2BodyDef();
    boxBodyDef.AddShape(boxObject);
    boxBodyDef.position.Set(capsuleX2, capsuleY2);
    world.CreateBody(boxBodyDef);

    boxObject = new b2BoxDef();
    boxObject.extents.Set(2, 95);
    boxObject.density = 0.0;

    let capsuleX3 = 128;
    let capsuleY3 = 380;

    boxBodyDef = new b2BodyDef();
    boxBodyDef.AddShape(boxObject);
    boxBodyDef.position.Set(capsuleX3, capsuleY3);
    world.CreateBody(boxBodyDef);



    // Create a payload
    //crankBoxDef.density = 2.0;
    //crankBodyDef.position.Set(positionX, 10);
    //world.CreateBody(crankBodyDef);

}
demos.InitWorlds.push(demos.crank.initWorld);