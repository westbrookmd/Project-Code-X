// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Display navigation menu when user clicks on it
function showMenu() {
    var links = document.getElementById("myLinks");
    if (links.style.display === "block") {
        links.style.display = "none";
    }
    else {
        links.style.display = "block";
    }
}