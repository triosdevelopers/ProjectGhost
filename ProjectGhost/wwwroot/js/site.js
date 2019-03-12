// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.



var mjpeg_img;

function reload_img() {
	mjpeg_img.src = "http://10.2.114.26:8080/cam.jpg?time=" + new Date().getTime();
}
function error_img() {
	setTimeout("mjpeg_img.src = 'http://10.2.114.26:8080/cam.jpg?time=' + new Date().getTime();", 100);
}
function init() {
	mjpeg_img = document.getElementById("mjpeg_dest");
	mjpeg_img.onload = reload_img;
	mjpeg_img.onerror = error_img;
	reload_img();
}


//--------------- for ghost animation ---------------//
var isLeft = false;
var isRight = false;
var isDown = false;
var isUp = false;
var isOpen = false;

function goLeft() {
	if (!isLeft) {
		$('#ghostLeft').animate(
			{
				left: 240
			}, 2500, 'swing');
		isLeft = true;
	}
	else {
		$('#ghostLeft').animate(
			{
				left: 418
			}, 2500, 'swing');
		isLeft = false;
	}
	if (isRight && isLeft && isUp && isDown) {
		isOpen = true;
	}
	else {
		isOpen = false;
	}
}

function goDown() {
	if (!isDown) {
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
	else {
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
	if (isRight && isLeft && isUp && isDown) {
		isOpen = true;
	}
	else {
		isOpen = false;
	}
}

function goUp() {
	if (!isUp) {
		$('#ghostTop').animate(
			{
				top: 9
			}, 2500, 'swing');
		isUp = true;
	}
	else {
		$('#ghostTop').animate(
			{
				top: 115
			}, 2500, 'swing');
		isUp = false;
	}
	if (isRight && isLeft && isUp && isDown) {
		isOpen = true;
	}
	else {
		isOpen = false;
	}
}

function goRight() {
	if (!isRight) {
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
	else {
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
	if (isRight && isLeft && isUp && isDown) {
		isOpen = true;
	}
	else {
		isOpen = false;
	}
}

function goCenter() {
	if (!isOpen) {
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
	else {
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

	if (optionChoice === "miscOptions") {
		setTimeout("init();", 100);
	}

	// Get all elements with class="tabcontent" and hide them
	options = document.getElementsByClassName("options");
	for (i = 0; i < options.length; i++) {
		options[i].style.display = "none";
	}

	// Get all elements with class="tablinks" and remove the class "active"
	tabOptions = document.getElementsByClassName("tabOptions");
	for (i = 0; i < tabOptions.length; i++) {
		tabOptions[i].className = tabOptions[i].className.replace(" active", "");
		document.getElementById('cameraScheduleBtn').style.background = "#f1f1f1";
		document.getElementById('cameraScheduleBtn').style.color = "#000";

		document.getElementById('miscOptionsBtn').style.background = "#f1f1f1";
		document.getElementById('miscOptionsBtn').style.color = "#000";

		document.getElementById('contactUsBtn').style.background = "#f1f1f1";
		document.getElementById('contactUsBtn').style.color = "#000";
	}
	// Show the current tab, and add an "active" class to the button that opened the tab
	document.getElementById(optionChoice).style.display = "block";
	document.getElementById(optionChoice + 'Btn').style.background = "#000";
	document.getElementById(optionChoice + 'Btn').style.color = "#fff";
	evt.currentTarget.optionChoice += " active";
}
//--------------- for tabs ---------------//
//--------------- for range time slider ---------------//
var rangeTimes = [];

$(".range-slider").slider({
	range: true,
	min: 0,
	max: 1440,
	values: [540, 1080],
	step: 15,
	slide: slideTime
});

function slideTime(event, ui) {
	if (event && event.target) {
		var $rangeslider = $(event.target);
		var $rangeday = $rangeslider.closest(".range-day");
		var rangeday_d = parseInt($rangeday.data('day'));
		var $rangecheck = $rangeday.find(":checkbox");
		var $rangetime = $rangeslider.next(".range-time");
	}

	if ($rangecheck.is(':checked')) {
		$rangeday.removeClass('range-day-disabled');
		$rangeslider.slider('enable');

		if (ui !== undefined) {
			var val0 = ui.values[0],
				val1 = ui.values[1];

			getOnOffTime(rangeday_d, val0, val1);

		} else {
			val0 = $rangeslider.slider('values', 0),
			val1 = $rangeslider.slider('values', 1);			
		}

		var minutes0 = parseInt(val0 % 60, 10),
			hours0 = parseInt(val0 / 60 % 24, 10),
			minutes1 = parseInt(val1 % 60, 10),
			hours1 = parseInt(val1 / 60 % 24, 10);
		if (hours1 === 0) hours1 = 24;

		rangeTimes[rangeday_d] = [getTime(hours0, minutes0), getTime(hours1, minutes1)];

		$rangetime.text(rangeTimes[rangeday_d][0] + ' - ' + rangeTimes[rangeday_d][1]);

	} else {
		$rangeday.addClass('range-day-disabled');
		$rangeslider.slider('disable');

		rangeTimes[rangeday_d] = [];

		$rangetime.text('OFF');
	}
}

function getTime(hours, minutes) {
	var time = null;
	minutes = minutes + "";
	if (minutes.length === 1) {
		minutes = "0" + minutes;
	}
	return hours + ":" + minutes;
}

$('.range-checkbox').on('change', function () {
	var $rangecheck = $(this);
	var $rangeslider = $rangecheck.closest('.range-day').find('.range-slider');
	slideTime({ target: $rangeslider });
});

slideTime({ target: $('#range-slider-1') });
slideTime({ target: $('#range-slider-2') });
slideTime({ target: $('#range-slider-3') });
slideTime({ target: $('#range-slider-4') });
slideTime({ target: $('#range-slider-5') });
slideTime({ target: $('#range-slider-6') });
slideTime({ target: $('#range-slider-7') });
//--------------- for range time slider ---------------//
//--------------- for range time slider values ---------------//
function getOnOffTime(day, val0, val1) {
	if (day == 1)
	{
        $('#OnTime1').val(val0);
        $('#OffTime1').val(val1);
	}

	if (day == 2)
	{
        $('#OnTime2').val(val0);
        $('#OffTime2').val(val1);
	}

	if (day == 3)
	{
        $('#OnTime3').val(val0);
        $('#OffTime3').val(val1);
	}

	if (day == 4)
	{
        $('#OnTime4').val(val0);
        $('#OffTime4').val(val1);
	}

	if (day == 5)
	{
        $('#OnTime5').val(val0);
        $('#OffTime5').val(val1);
	}

	if (day == 6)
	{
        $('#OnTime6').val(val0);
        $('#OffTime6').val(val1);
	}

	if (day == 7)
	{
        $('#OnTime7').val(val0);
        $('#OffTime7').val(val1);
	}
}
//--------------- for range time slider values ---------------//
//---------------  FOR RECORDING LENGTH SLIDER ---------------//
function getValue(myRange, output) {

    output.innerText = myRange.value;

	myRange.oninput = function () {
		output.innerText = this.value;
	};
}

function toggleValue(myRange, output) {

    if (myRange.value == 0) {
        output.innerText = "Off";
        myRange.oninput = function () {
            output.innerText = "Off";
        };
    }
    else if (myRange.value == 1) {
        output.innerText = "On";
        myRange.oninput = function () {
            output.innerText = "On";
        };
    }
    else {
        alert('not working at all');
    }
}

//---------------  FOR RECORDING LENGTH SLIDER  ---------------//
//---------------  FOR RECORDING ANIMATION---------------//

function recordingStarted()
{
    $('#mjpeg_dest').style.addClass = "videoRecording";
}



//---------------  FOR RECORDING ANIMATION---------------//
