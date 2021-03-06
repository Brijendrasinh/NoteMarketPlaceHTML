/* ===============================================
                Navigation
=============================================== */

/* Show And Hide White Navigation */
/* it is placed in bottom of body tag of index.html   
$(function () {

    ShowHideNav();
    // show hide nav on page load
    
    
    $(window).scroll(function () {

        // show hide nav on page load
        ShowHideNav();

    });
    
    function ShowHideNav() {
        if ($(window).scrollTop() > 50) {

            // show white nav
            $("nav").addClass("white-nav-top");

            //show dark logo
            $(".navbar-brand img").attr("src", "../images/pre-login/logo.png");

            //show back to top button
            $("#back-to-top").fadeIn();

        } else {

            // hide white nav
            $("nav").removeClass("white-nav-top");

            //hide dark logo
            $(".navbar-brand img").attr("src", "../images/pre-login/top-logo.png");

            //hide back to top button
            $("#back-to-top").fadeOut();

        }
    }

});
*/
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


$(function () {
    $('#sign-up').on("click", function () {
        let valid = true;
        $('[required]').each(function () {
            if ($(this).is(':invalid') || !$(this).val()) valid = false;
        });
        if (!valid) {
            alert("error please fill all fields!");
        } else {
            document.getElementById('complete-sign-up').style.display = "block";
        }
    });

});

/* ===============================================
                Show or hide Faq header
=============================================== 

$(function () {
    $(".btn-link").click(function () {
        var input = $($(this).attr("data-target"));
        var newinput = $(input.attr("aria-labelledby"));
        
        newinput.addClass('hide');
    });
});

$(function () {
    $(".btn-minus").click(function(){
        var input = $($(this).attr("data-target"));
        var secondinput = $($(this).attr("data-labelid"));
        input.removeClass('hide');
        secondinput.removeClass('show')
    });
});

*/


/* ===============================================
                Data table plugin
=============================================== */

