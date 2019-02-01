// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

//--------------- for ghost animation ---------------//
var isLeft = false;
var isRight = false;
var isDown = false;
var isUp = false;
var isOpen = false; // for center thing.

function goLeft()
{
	if (!isLeft)
	{
		$('#ghostLeft').animate(
			{
				left: 240
			}, 2500, 'swing');
		isLeft = true;
	}
	else
	{
		$('#ghostLeft').animate(
			{
				left: 418
			}, 2500, 'swing');
		isLeft = false;
	}
	if (isRight && isLeft && isUp && isDown)
	{
		isOpen = true;
	}
	else
	{
		isOpen = false;
	}
}


function goDown()
{
	if (!isDown)
	{
		$('#ghostBottomBack').animate(
			{
				top: 472
			}, 2500, 'swing');
		$('#ghostBottomFront').animate(
			{
				top: 510
			}, 2500, 'swing');
		isDown = true;
	}
	else
	{
		$('#ghostBottomBack').animate(
			{
				top: 371
			}, 2500, 'swing');
		$('#ghostBottomFront').animate(
			{
				top: 410
			}, 2500, 'swing');
		isDown = false;
	}
	if (isRight && isLeft && isUp && isDown)
	{
		isOpen = true;
	}
	else {
		isOpen = false;
	}
}

function goUp()
{
	if (!isUp)
	{
		$('#ghostTop').animate(
			{
				top: 5
			}, 2500, 'swing');
		isUp = true;
	}
	else
	{
		$('#ghostTop').animate(
			{
				top: 115
			}, 2500, 'swing');
		isUp = false;
	}	
	if (isRight && isLeft && isUp && isDown)
	{
		isOpen = true;
	}
	else
	{
		isOpen = false;
	}
}

function goRight()
{
	if (!isRight)
	{
		$('#ghostRightFront').animate(
			{
				left: 900
			}, 2500, 'swing');

		$('#ghostRightBack').animate(
			{
			left: 830
			}, 2500, 'swing');
		isRight = true;
	}
	else
	{
		$('#ghostRightFront').animate(
			{
				left: 707
			}, 2500, 'swing');

		$('#ghostRightBack').animate(
			{
			left: 638
			}, 2500, 'swing');
		isRight = false;
	}
	if (isRight && isLeft && isUp && isDown)
	{
		isOpen = true;
	}
	else
	{
		isOpen = false;
	}
}

function goCenter()
{
	if (!isOpen)
	{
		isLeft = false;
		isRight = false;
		isUp = false;
		isDown = false;
		goRight();
		goLeft();
		goUp();
		goDown();
		isOpen = true;

	}
	else
	{
		isLeft = true;
		isRight = true;
		isUp = true;
		isDown = true;
		goRight();
		goLeft();
		goUp();
		goDown();
		isOpen = false;
	}
}
//--------------- for ghost animation ---------------//

//--------------- for starfield animation ---------------//
function Starfield() {
	this.fps = 40;
	this.canvas = null;
	this.width = 0;
	this.minVelocity = 10;
	this.maxVelocity = 80;
	this.stars = 100;
	this.intervalId = 0;
}

Starfield.prototype.initialise = function (div) {
	this.containerDiv = div;
	this.width = window.innerWidth;
	this.height = window.innerHeight;

	let self = this;

	let canvas = document.createElement("canvas");
	div.appendChild(canvas);
	this.canvas = canvas;
	this.canvas.width = this.width;
	this.canvas.height = this.height;

	window.addEventListener("resize", function resize(event) {
		self.width = window.innerWidth;
		self.height = window.innerHeight;
		self.canvas.width = self.width;
		self.canvas.height = self.height;
		self.draw();
	});
};

Starfield.prototype.start = function () {
	let stars = [];
	for (let i = 0; i < this.stars; i++) {
		stars[i] = new Star(
			Math.random() * this.height,
			Math.random() * this.width,
			Math.random() * 3 + 1,
			Math.random() * (this.maxVelocity - this.minVelocity) +
			this.minVelocity
		);
	}
	this.stars = stars;

	let self = this;
	this.intervalId = setInterval(function () {
		self.update();
		self.draw();
	}, 1000 / this.fps);
};

Starfield.prototype.stop = function () {
	clearInterval(this.intervalId);
};

Starfield.prototype.update = function () {
	let dt = 1 / this.fps;

	for (let i = 0; i < this.stars.length; i++) {
		let star = this.stars[i];
		star.y += dt * star.velocity;
		if (star.y > this.width) {
			this.stars[i] = new Star(
				Math.random() * this.height,
				0,
				Math.random() * 3 + 1,
				Math.random() * (this.maxVelocity - this.minVelocity) +
				this.minVelocity
			);
		}
	}
};

Starfield.prototype.draw = function () {
	let ctx = this.canvas.getContext("2d");

	ctx.fillStyle = "#000";
	ctx.fillRect(0, 0, this.width, this.height);

	ctx.fillStyle = "#fff";
	for (let i = 0; i < this.stars.length; i++) {
		let star = this.stars[i];
		ctx.fillRect(star.y, star.x, star.size, star.size);
	}
};

function Star(x, y, size, velocity) {
	this.x = x;
	this.y = y;
	this.size = size;
	this.velocity = velocity;
}

function randomize() {
	starfield.stop();
	starfield.stars = Math.random() * 1000 + 50;
	starfield.minVelocity = Math.random() * 30 + 5;
	starfield.maxVelocity = Math.random() * 50 + starfield.minVelocity;
	starfield.start();
}

let container = document.getElementById("stars");
let starfield = new Starfield();
starfield.initialise(container);
starfield.start();
//--------------- for starfield animation ---------------//


//--------------- for login and account creation button thing ---------------//
$(document).ready(function(){
	$('#login-trigger').click(function () {
		$(this).next('#login-content').slideToggle();
		$(this).toggleClass('active');

		if ($(this).hasClass('active')) $(this).find('span').html('&#x25B2;');
		else $(this).find('span').html('&#x25BC;');
	});
});
//--------------- for login and account creation button thing ---------------//

//--------------- for tabs ---------------//
function openOptions(evt, optionChoice) {
  // Declare all variables
  var i, options, tabOptions;

  // Get all elements with class="tabcontent" and hide them
  options = document.getElementsByClassName("options");
  for (i = 0; i < options.length; i++) {
    options[i].style.display = "none";
  }

  // Get all elements with class="tablinks" and remove the class "active"
  tabOptions = document.getElementsByClassName("tabOptions");
  for (i = 0; i < tabOptions.length; i++) {
    tabOptions[i].className = tabOptions[i].className.replace(" active", "");
  }

  // Show the current tab, and add an "active" class to the button that opened the tab
  document.getElementById(optionChoice).style.display = "block";
  evt.currentTarget.optionChoice += " active";
}
//--------------- for tabs ---------------//