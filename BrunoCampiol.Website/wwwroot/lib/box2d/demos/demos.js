// Lates file to be loaded

var initId = 0;
var world = createWorld();
var ctx;
var canvasWidth;
var canvasHeight;
var canvasTop;
var canvasLeft;

function setupWorld(did) {
    if (!did) did = 0;
    world = createWorld();
    initId += did;
    initId %= demos.InitWorlds.length;
    if (initId < 0) initId = demos.InitWorlds.length + initId;
    demos.InitWorlds[initId](world);
}
function setupNextWorld() { setupWorld(1); }
function setupPrevWorld() { setupWorld(-1); }
function step(cnt) {
    var stepping = false;
    var timeStep = 1.0 / 60;
    var iteration = 1;
    world.Step(timeStep, iteration);
    ctx.clearRect(0, 0, canvasWidth, canvasHeight);
    drawWorld(world, ctx);
    setTimeout('step(' + (cnt || 0) + ')', 10);
}

// Returns the mouse position inside canvas
function getMousePos(canvas, evt) {
    var rect = canvas.getBoundingClientRect();
    return {
        x: evt.clientX - rect.left,
        y: evt.clientY - rect.top
    };
}

// Retrieves the user selected drop quantity (radiobuttons)
function getDropQuantity() {
    var radios = document.getElementsByName('dropQuantity');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            return radios[i].value;
        }
    }
}

// Retrieves the drop type (radiobuttons)
function getDropType() {
    var radios = document.getElementsByName('dropType');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            return radios[i].value;
        }
    }
}

function InitializeBox2D() {
    setupWorld();
    ctx = $('canvas').getContext('2d');
    var canvasElm = $('canvas');
    canvasWidth = parseInt(canvasElm.width);
    canvasHeight = parseInt(canvasElm.height);
    canvasTop = parseInt(canvasElm.style.top);
    canvasLeft = parseInt(canvasElm.style.left);

    // TODO: change the canva size when resizing. Make it dynamic   

    var canvasObject = document.getElementById("canvas");
    var canvasContext = canvasObject.getContext('2d');
    var canvasPosition = $('canvas').getBoundingClientRect();

    // Global variables for mouse position
    var positionX = 0;
    var positionY = 0;

    // Updates the current mouse position in canvas world (box2d)
    canvasObject.addEventListener('mousemove', function (evt) {
        var mousePos = getMousePos(canvas, evt);
        positionX = mousePos.x;
        positionY = mousePos.y;
        //var messageMouse = 'Mouse position: ' + mousePos.x + ',' + mousePos.y;
        //document.getElementById('mouseYpossss').innerHTML = messageMouse;
    }, false);

    // Event listner for 'click'
    Event.observe('canvas', 'click', function (e) {

        for (i = 0; i < getDropQuantity(); i++) {

            // 0 = circle ; 1 = square
            if (getDropType() == 0) {
                createBall(world, positionX, positionY, 10, 0.2, 0.2);
            }
            else {
                createBox(world, positionX, positionY, 10, 10, false);
            }

            //setupNextWorld();
            //if (Math.random() < 0.5)    
        }

    });

    //Event.observe('canvas', 'contextmenu', function (e) {
    //    if (e.preventDefault) e.preventDefault();
    //    setupPrevWorld();
    //    return false;
    //});

    step();
}


// Start point for Box2D simulator
(function () {
    InitializeBox2D();
})();