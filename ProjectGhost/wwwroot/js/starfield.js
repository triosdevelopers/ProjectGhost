//--------------- for starfield animation ---------------//
function Starfield() {
	this.fps = 40;
	this.canvas = null;
	this.width = 0;
	this.minVelocity = 20;
	this.maxVelocity = 80;
	this.stars = 100;
	this.intervalId = 0;
}

Starfield.prototype.initialise = function (div) {
	this.containerDiv = div;
	this.width = window.innerWidth - 16;
	this.height = window.innerHeight + 200;

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