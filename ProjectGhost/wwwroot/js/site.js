// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// for login and account creation button thing
$(document).ready(function(){
    $('#login-trigger').click(function(){
        $(this).next('#login-content').slideToggle();
        $(this).toggleClass('active');

        if ($(this).hasClass('active')) $(this).find('span').html('&#x25B2;')
        else $(this).find('span').html('&#x25BC;')
    })
});

// for tabs
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



var slider = document.getElementById("myRange");
var output = document.getElementById("demo");
output.innerHTML = slider.value; // Display the default slider value

// Update the current slider value (each time you drag the slider handle)
slider.oninput = function () {
    output.innerHTML = this.value;
}