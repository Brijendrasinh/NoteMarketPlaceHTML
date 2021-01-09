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











