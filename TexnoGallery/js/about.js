$(".two-menu").click(function(){
    $(".menu-vertical").toggleClass("open");
});

/****/
$(window).scroll(function(){
    if ($(window).scrollTop() >= 100) {
        $('.main-nav').addClass('fixed-header');
        $('nav div').addClass('visible-title');
    }
    else {
        $('.main-nav').removeClass('fixed-header');
        $('nav div').removeClass('visible-title');
    }
});