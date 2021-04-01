/* ===============================================
                Mobile Menu
=============================================== */

$(function () {

    // Show mobile nav
    $("#mobile-nav-open-btn").click(function () {
        $("#mobile-nav").css("height", "100%");
    });

    // Hide mobile nav
    $("#mobile-nav-close-btn").click(function () {
        $("#mobile-nav").css("height", "0%");
    });

});

/* ===============================================
                Show or hide password
=============================================== */

$(function () {
    $(".toggle-password").click(function () {
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
});

/* Password is right or wrong method */


function validate() {
    var pword = document.getElementById('exampleInputLoginPassword').value;

    if (pword !== "admin") {
        document.getElementById('exampleInputLoginPassword').style.border = "1px solid #ff5e5e"
        document.getElementById('wrongPasswordError').style.display = "block";
    } else if (pword == "admin") {
        document.getElementById('exampleInputLoginPassword').style.border = "1px solid #6255a5"
        document.getElementById('wrongPasswordError').style.display = "none";
        alert(" correct password ");
    }
}
