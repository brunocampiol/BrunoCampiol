// This is the 1rst world

demos.top = {};
demos.top.createBall = function(world, x, y, rad, fixed) {
	var ballSd = new b2CircleDef();
	if (!fixed) ballSd.density = 1.0;
	ballSd.radius = rad || 10;
    ballSd.restitution = 0.2;
    ballSd.friction = 9999999;
	var ballBd = new b2BodyDef();
	ballBd.AddShape(ballSd);
	ballBd.position.Set(x,y);
	return world.CreateBody(ballBd);
};
demos.top.createPoly = function(world, x, y, points, fixed) {
	var polySd = new b2PolyDef();
	if (!fixed) polySd.density = 1.0;
	polySd.vertexCount = points.length;
	for (var i = 0; i < points.length; i++) {
		polySd.vertices[i].Set(points[i][0], points[i][1]);
	}
	var polyBd = new b2BodyDef();
	polyBd.AddShape(polySd);
	polyBd.position.Set(x,y);
	return world.CreateBody(polyBd)
};
demos.top.createRotation = function (b2BodyDef, x, y, speed) {
    // Creates a joint and makes spinning
    var ball2Rotation = new b2RevoluteJointDef();
    ball2Rotation.anchorPoint.Set(x, y);
    ball2Rotation.body1 = world.m_groundBody;
    ball2Rotation.body2 = b2BodyDef;
    ball2Rotation.motorSpeed = speed;
    ball2Rotation.motorTorque = 5000000000000.0;
    ball2Rotation.enableMotor = true;
    world.CreateJoint(ball2Rotation);
};
// Creates the initial world
demos.top.initWorld = function (world) {

    var ballRadius = 30;
    var ballSpeed = 0.4 * Math.PI;

    var ball2X = 223;
    var ball2Y = 460;

    var ball3X = 163;
    var ball3Y = 440;

    var ball4X = 103;
    var ball4Y = 420;

    var ball1X = 43;
    var ball1Y = 400;
   

    var ball1Body = demos.top.createBall(world, ball1X, ball1Y, ballRadius, false);
    var ball2Body = demos.top.createBall(world, ball4X, ball4Y, ballRadius, false);
    var ball3Body = demos.top.createBall(world, ball3X, ball3Y, ballRadius, false);
    var ball4Body = demos.top.createBall(world, ball2X, ball2Y, ballRadius, false);

    demos.top.createRotation(ball1Body, ball1X, ball1Y, -ballSpeed);
    demos.top.createRotation(ball4Body, ball2X, ball2Y, -ballSpeed);
    demos.top.createRotation(ball3Body, ball3X, ball3Y, -ballSpeed);
    demos.top.createRotation(ball2Body, ball4X, ball4Y, -ballSpeed);

    var rectangleX = 400;
    var rectangleY = 330;

    // Creates the triangle with a joint
    var rectangleBody = demos.top.createPoly(world, rectangleX, rectangleY, [[0, 0], [100, 30], [-100, 30]]);
    var jointDef = new b2RevoluteJointDef();
    jointDef.body1 = rectangleBody;
    jointDef.body2 = world.GetGroundBody();
    jointDef.anchorPoint = new b2Vec2(rectangleX, rectangleY);
    world.CreateJoint(jointDef);    

    // The object motor size and density
    var boxObject = new b2BoxDef();
    boxObject.extents.Set(5, 80);
    boxObject.density = 1.0;

    var fanX = 710;
    var fanY = 410;

    var bodyDef = new b2BodyDef();
    bodyDef.AddShape(boxObject);
    bodyDef.position.Set(fanX, fanY);
    var fanBody = world.CreateBody(bodyDef);

    // Creates a joint and makes spinning
    var fanRotation = new b2RevoluteJointDef();
    fanRotation.anchorPoint.Set(fanX, fanY);
    fanRotation.body1 = world.m_groundBody;
    fanRotation.body2 = fanBody;
    fanRotation.motorSpeed = ballSpeed * 4;
    fanRotation.motorTorque = 5000000000000.0;
    fanRotation.enableMotor = true;
    world.CreateJoint(fanRotation);
};
demos.InitWorlds.push(demos.top.initWorld);