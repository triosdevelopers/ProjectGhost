// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


//--------------- for ghost animation ---------------//
var isLeft = false;
var isRight = false;
var isDown = false;
var isUp = false;
var isOpen = false;

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
				top: 9
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
				left: 830
			}, 2500, 'swing');

		$('#ghostRightBack').animate(
			{
			left: 760
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
//--------------- for time slider ---------------//

//--------------- for time slider ---------------//