// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Display navigation menu when user clicks on it
function myFunction() {
    var x = document.getElementById("myTopnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }
}

function displayAdminLinks() {
    var x = document.getElementById("adminLinks");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}

function addRow() {
    var tbl = document.getElementById("purchLineTable");
    var row = tbl.insertRow(-1);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    cell1.innerHTML = "<input type=\"text\" id=\"itemName\" name=\"name\" value=\"\" />";
    cell2.innerHTML = "<input type=\"text\" id=\"qnty\" name=\"qnty\" value=\"\" />";
    cell3.innerHTML = "<input type=\"text\" id=\"cost\" name=\"cost\" value=\"\" />";
}